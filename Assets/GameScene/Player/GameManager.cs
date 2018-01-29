using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	public float GameDuration = 300;

	public float Time_Passed = 0;

	public TeamBase team1;

	public TeamBase team2;

	public Text t1_energy;

	public Text t2_energy;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	IEnumerator EndGame()
	{
		while (Time_Passed <= GameDuration)
		{

			yield return new WaitForSeconds(1);
			Time_Passed++;
		}
		team1.DisablePlayers();
		team2.DisablePlayers();


		if (team1.Energy > team2.Energy)
		{

		}
		else if (team1.Energy < team2.Energy)
		{

		}
		else
		{

		}

	}
}
