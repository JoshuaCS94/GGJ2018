using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour {

	public float GameDuration = 300;

	public float Time_Passed = 0;

	public Text text;

	// Use this for initialization
	void Start ()
	{
		text = GetComponentInChildren<Text>();
		text.text = GameDuration.ToString();
	}

	public void Play()
	{
		StartCoroutine("EndGame");
		Play();
	}

	IEnumerator EndGame()
	{
		while (Time_Passed <= GameDuration - 5)
		{
			text.text = (GameDuration - Time_Passed).ToString();
			yield return new WaitForSeconds(1);
			Time_Passed++;
		}
		while (Time_Passed <= GameDuration)
		{
			//Play Beep
			yield return new WaitForSeconds(1);
		}
		text.text = "GAME OVER";
	}


}
