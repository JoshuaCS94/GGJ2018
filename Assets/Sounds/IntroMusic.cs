using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroMusic : MonoBehaviour
{
	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
		SceneManager.sceneLoaded += FinishMusic;
	}

	void FinishMusic(Scene scene, LoadSceneMode mode)
	{
		if (scene.buildIndex == 3)
		{
			GetComponent<AudioSource>().DOFade(0, 1).OnComplete(() =>
				SceneManager.sceneLoaded -= FinishMusic).OnComplete(() =>
				Destroy(gameObject));
		}
	}
}
