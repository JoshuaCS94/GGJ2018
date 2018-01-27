﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamBase : MonoBehaviour
{
	public float TeamEnergy = 0;

	//TODO: luz en el sprite del color del player
	private float TeamID;

	public GameObject[] TeamSpawnPoints;
	public LayerMask layer;

	public float ReSpawnUpdateTime = 0.2f;

	private int TeamMembersCount = 0;
	public BoxCollider2D bc2d;


	public GameObject TestPlayer;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			AddPlayer(TestPlayer);
		}
	}

	public void AddPlayer(GameObject player)
	{
		if (TeamMembersCount == 4)
		{
			return;
		}

		bc2d = player.GetComponentInChildren<BoxCollider2D>();
		var p = player.AddComponent<TeamMember>();
		p.identifier = TeamMembersCount;

		SpawnPlayer(p);

		TeamMembersCount++;
	}



	public void SpawnPlayer(TeamMember player)
	{

		var size = bc2d.bounds.extents;
		var s_point = TeamSpawnPoints[player.identifier];

		player.gameObject.transform.position = s_point.transform.position;



		var pos = new Vector3(s_point.transform.position.x, s_point.transform.position.y + size.y);
		var collision = Physics2D.OverlapBox(pos, size, layer);
		if (collision != null)
		{
			StartCoroutine("WaitForSpawn", player);
		}



	}

	IEnumerator WaitForSpawn(TeamMember player)
	{
		var size = bc2d.bounds.extents;
		var s_point = TeamSpawnPoints[player.identifier];

		var pos = new Vector3(s_point.transform.position.x, s_point.transform.position.y + size.y);
		var collision = Physics2D.OverlapBox(pos, size, layer);
		while (collision != null)
		{
			yield return new WaitForSeconds(ReSpawnUpdateTime);
		}


	}

	void InvokePlayer(TeamMember player)
	{
		var size = bc2d.bounds.extents;

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
