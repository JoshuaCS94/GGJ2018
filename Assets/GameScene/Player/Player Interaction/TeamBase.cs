using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamBase : MonoBehaviour
{
	public float TeamEnergy = 0;

	//TODO: luz en el sprite del color del player
	private float TeamID;

	public GameObject[] TeamSpawnPoints;

	private int TeamMembersCount = 0;
	public BoxCollider2D bc2d;

	public GameObject TestPlayer;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Space))
		{
			AddPlayer(TestPlayer	);
		}
	}

	public void AddPlayer(GameObject player)
	{
		if (TeamMembersCount == 4)
		{
			return;
		}
		bc2d = player.GetComponent<BoxCollider2D>();
		var p = player.AddComponent<TeamMember>();
		p.identifier = TeamMembersCount;

		SpawnPlayer(p);

		TeamMembersCount++;
	}



	public void SpawnPlayer(TeamMember player)
	{

	}
}
