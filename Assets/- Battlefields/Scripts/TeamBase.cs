using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TeamBase : MonoBehaviour
{
	//TODO: luz en el sprite del color del player
	private float TeamID;

	public float Energy = 0;

	public GameObject[] TeamSpawnPoints;
	public LayerMask layer;

	public float TimeToRespawn = 2;
	public float ReSpawnUpdateTime = 0.2f;

	private int TeamMembersCount = 0;
	private BoxCollider2D bc2d;
	public List<TeamMember> players = new List<TeamMember>();

	public Transform initial_spawn_point;

	public GameObject[] TestPlayers;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			foreach (var player in players)
			{
				Sequence s = DOTween.Sequence()
					.AppendCallback(() => player.portal.StartAnimation())
					.AppendInterval(TimeToRespawn)
					.AppendCallback(() => SpawnPlayer(player))
					.OnComplete(() => player.portal.StopAnimation());
			}
		}
	}

	public void AddPlayer(GameObject player)
	{
		if (TeamMembersCount == 4)
		{
			return;
		}
//		player.transform.position =
		bc2d = player.GetComponentInChildren<BoxCollider2D>();
		var c = player.GetComponent<PlayerData>().Color;
		var portal = TeamSpawnPoints[TeamMembersCount].GetComponent<PortalAnimationController>();
		portal.ChangeColor(c);
		var mov = player.transform.Find("Movement").gameObject;
		var p = mov.AddComponent<TeamMember>();
		mov.AddComponent<SpikeWeakness>().Team = this;

		p.identifier = TeamMembersCount;
		p.portal = portal;
		p.team = this;
		players.Add(p);

		TeamMembersCount++;

		player.transform.position = initial_spawn_point.transform.position;
		SpawnPlayer(p);
	}

	public void AddPlayers(GameObject[] players)
	{
		foreach (var player in players)
		{
			AddPlayer(player);
		}
	}

	public void SpawnPlayer(TeamMember player)
	{

		SetPlayerPos(player);
		var size = bc2d.bounds.extents;

		var s_point = TeamSpawnPoints[player.identifier];

		var pos = new Vector3(s_point.transform.position.x, s_point.transform.position.y + size.y);
		var collision = Physics2D.OverlapBox(pos, size, layer);

		if (collision != null && collision.gameObject != player.gameObject )
		{
//			print(collision.gameObject);
			StartCoroutine("WaitForSpawn", player);
			return;
		}

		InvokePlayer(player);
	}

	public void SpawnAllPLayers()
	{
		foreach (var p in players)
		{
			SpawnPlayer(p);
		}
	}

	void SetPlayerPos(TeamMember player)
	{
		var size = bc2d.bounds.extents;
		var s_point = TeamSpawnPoints[player.identifier];

		player.gameObject.transform.SetZ(1);
		player.gameObject.transform.SetX(s_point.transform.position.x);
		player.gameObject.transform.SetY(s_point.transform.position.y - size.y);
	}

	public void KillPlayer(TeamMember player)
	{
		SetPlayerPos(player);

		Sequence s = DOTween.Sequence()
			.AppendCallback(() => player.portal.StartAnimation())
			.AppendInterval(TimeToRespawn)
			.AppendCallback(() => SpawnPlayer(player))
			.OnComplete(() => player.portal.StopAnimation());
	}

	IEnumerator WaitForSpawn(TeamMember player)
	{

		var size = bc2d.bounds.extents;
//		print("size =" + size.y);

		var s_point = TeamSpawnPoints[player.identifier];

		var pos = new Vector3(s_point.transform.position.x, s_point.transform.position.y + size.y);
		var collision = Physics2D.OverlapBox(pos, size, layer);
		while (collision != null)
		{
			if(collision.gameObject.GetComponent<TeamMember>().team == this)
				break;
			print("waiting for" + collision.gameObject);
			yield return new WaitForSeconds(ReSpawnUpdateTime);
			collision = Physics2D.OverlapBox(pos, size, layer);
		}

		InvokePlayer(player);
	}

	void InvokePlayer(TeamMember player)
	{
		var pm = player.GetComponent<PlayerMovement>();
		var rb = player.GetComponent<Rigidbody2D>();

		var size = bc2d.bounds.extents;
		var s_point = TeamSpawnPoints[player.identifier];

		player.transform.DOMoveY(s_point.transform.position.y + 4 * size.y, 0.2f).OnComplete(() =>
		{
//			print("meh");
			pm.enabled = true;
			rb.isKinematic = false;
		});
	}

	public void DischargeEnergy(EnergyCarrier carrier)
	{
		TeamMember player = carrier.GetComponent<TeamMember>();
		Energy += carrier.Energy;
		carrier.Energy = 0;
		//TODO: UI
	}

	public void DisablePlayers()
	{
		foreach (var player in players)
		{
			player.GetComponent<PlayerMovement>().enabled = false;
			player.GetComponent<Rigidbody2D>().isKinematic = true;
			player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}
	}
}

static class externals
{
	public static void SetX(this Transform t, float value)
	{
		t.position = new Vector3(value, t.position.y, t.position.z);
	}
	public static void SetY(this Transform t, float value)
	{
		t.position = new Vector3(t.position.x, value, t.position.z);
	}
	public static void SetZ(this Transform t, float value)
	{
		t.position = new Vector3(t.position.x, t.position.y, value);
	}
}
