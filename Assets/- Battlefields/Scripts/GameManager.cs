using System.Collections;
using System.Collections.Generic;
using Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	public float GameDuration = 300;

	public float Time_Passed = 0;

	public TeamBase team1;

	public TeamBase team2;

	public Text t1_energy;

	public Text t2_energy;

	public bool finished = false;

	// Use this for initialization
	void Start () {
		Play();
	}

	// Update is called once per frame
	void Update()
	{
		if (!finished)
		{
			t1_energy.text = team1.Energy.ToString();
			t2_energy.text = team2.Energy.ToString();
		}
	}

	void Play()
	{
		StartCoroutine("EndGame");
	}

	IEnumerator EndGame()
	{
		while (Time_Passed <= GameDuration)
		{

			yield return new WaitForSeconds(1);
			Time_Passed++;
		}
		finished = true;
		team1.DisablePlayers();
		team2.DisablePlayers();


		if (team1.Energy > team2.Energy)
		{
			t1_energy.text = "WINNER";
			t2_energy.text = "LOOSER";
		}
		else if (team1.Energy < team2.Energy)
		{
			t2_energy.text = "WINNER";
			t1_energy.text = "LOOSER";
		}
		else
		{
			t1_energy.text = "DRAW";
			t2_energy.text = "DRAW";
		}
		yield return new WaitForSeconds(5);

		SceneManager.LoadScene("Lobby");
	}
}
