using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Scripts;
using UnityEngine;

public class TeamBase : MonoBehaviour
{
	//TODO: luz en el sprite del color del player
	private float TeamID;

	public float Energy = 0;

	public GameObject[] TeamSpawnPoints;
	public LayerMask BlockSpawningMask;

	public float TimeToBorn = 3;
	public float TimeToRespawn = 2;
	public float ReSpawnUpdateTime = 0.2f;

	private int TeamMembersCount = 0;
	private BoxCollider2D bc2d;
	public List<TeamMember> players = new List<TeamMember>();

//	public Transform initial_spawn_point;

	private void Start()
	{
		foreach (var player in players)
		{
			AddPlayer(player.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			foreach (var player in players)
			{
				DOTween.Sequence()
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
		var c = player.GetComponent<PlayerData>().playerColor;
		var portal = TeamSpawnPoints[TeamMembersCount].GetComponent<PortalAnimationController>();
		portal.ChangeColor(c);
		var mov = player.transform.Find("Movement").gameObject;
		var p = mov.AddComponent<TeamMember>();
		mov.AddComponent<TouchWeakness>().Team = this;

		p.identifier = TeamMembersCount;
		p.portal = portal;
		p.team = this;
		players.Add(p);

		TeamMembersCount++;

//		player.transform.position = initial_spawn_point.transform.position;

		DisablePlayer(p);

		SpawnPlayer(p);
	}

	public void AddPlayers(GameObject[] plyers)
	{
		foreach (var player in plyers)
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
		var collision = Physics2D.OverlapBox(pos, size, BlockSpawningMask);

		if (collision != null && collision.gameObject != player.gameObject )
		{
			StartCoroutine("WaitForSpawn", player);
			return;
		}

		InvokePlayer(player);
	}

	//TOdo: en un futuro llamar a esta funcion cuando se vayan a spawnear todos los players, y quitar del AddPlayer el llamado a Spawn
	public void SpawnAllPlayers()
	{
		DOTween.Sequence()
			/*.AppendCallback(() => sonido de que va a empezar la cosa que dura TimeToBorn)*/
			.AppendInterval(TimeToBorn)
			.AppendCallback(() =>
			{
				foreach (var player in players)
				{
					SpawnPlayer(player);
				}
			});
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

		DOTween.Sequence()
			.AppendCallback(() => player.portal.StartAnimation())
			.AppendInterval(TimeToRespawn)
			.AppendCallback(() => SpawnPlayer(player))
			.OnComplete(() => player.portal.StopAnimation());
	}

	IEnumerator WaitForSpawn(TeamMember player)
	{

		var size = bc2d.bounds.extents;

		var s_point = TeamSpawnPoints[player.identifier];

		var pos = new Vector3(s_point.transform.position.x, s_point.transform.position.y + size.y);
		var collision = Physics2D.OverlapBox(pos, size, BlockSpawningMask);
		while (collision != null)
		{
			if(collision.gameObject.GetComponent<TeamMember>().team == this)
				break;
			yield return new WaitForSeconds(ReSpawnUpdateTime);
			collision = Physics2D.OverlapBox(pos, size, BlockSpawningMask);
		}

		InvokePlayer(player);
	}

	void InvokePlayer(TeamMember player)
	{
		var size = bc2d.bounds.extents;
		var s_point = TeamSpawnPoints[player.identifier];

		player.transform.DOMoveY(s_point.transform.position.y + 4 * size.y, 0.2f).OnComplete(() =>
		{
			EnablePlayer(player);
		});
	}

	public void DischargeEnergy(EnergyCarrier carrier)
	{
		TeamMember player = carrier.GetComponent<TeamMember>();
		Energy += carrier.Energy;
		carrier.Energy = 0;
		//TODO: UI
	}

	public void DisablePlayer(TeamMember player)
	{
		player.GetComponent<PlayerMovement>().enabled = false;
		player.GetComponent<PlayerBurst>().enabled = false;
		player.GetComponent<Rigidbody2D>().isKinematic = true;
		player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
//		player.gameObject.SetActive(false);
	}

	public void EnablePlayer(TeamMember player)
	{
		player.GetComponent<PlayerMovement>().enabled = true;
		player.GetComponent<PlayerBurst>().enabled = true;
		player.GetComponent<Rigidbody2D>().isKinematic = false;
//		player.gameObject.SetActive(true);
	}

	public void DisablePlayers()
	{
		foreach (var player in players)
		{
			DisablePlayer(player);
		}
	}

	public void EnablePlayers()
	{
		players.ForEach(EnablePlayer);
	}
}

static class Externals
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
