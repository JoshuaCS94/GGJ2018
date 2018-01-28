using UnityEngine;
using UnityEngine.SceneManagement;

public class WellcomeManager : MonoBehaviour
{
	public void ExitGame()
	{
		Application.Quit();
	}

	public void StartGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
