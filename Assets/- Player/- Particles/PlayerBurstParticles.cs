using UnityEngine;

public class PlayerBurstParticles : MonoBehaviour
{
	public PlayerData data;
	public PlayerMovement movement;
	public PlayerBurst burst;
	private ParticleSystem particles;
	public float StopTime;

	private PlayerBurst.MyEvent action;
	private ParticleSystem.MinMaxCurve initialRate;

	private Vector3 initialPos;

	void Start ()
	{
		initialPos = transform.localPosition;
		action = PlayParticles;
		burst.BurstEvents += action;
		particles = GetComponent<ParticleSystem>();
		particles.Stop();
		var color = particles.main;
		color.startColor = data.Color;
	}

	void PlayParticles(KeyCode direction)
	{
		particles.Play();

		Quaternion rotation = Quaternion.identity;

		switch (direction)
		{
			case KeyCode.LeftArrow:
			{
				transform.localPosition = -initialPos;
				break;
			}

			case KeyCode.RightArrow:
			{
				transform.localPosition = initialPos;
				break;
			}

			case KeyCode.UpArrow:
			{
				rotation = Quaternion.Euler(0, 0, 90);
				transform.localPosition = new Vector3(0, 0, initialPos.z);
				break;
			}
			case KeyCode.DownArrow:
			{
				rotation = Quaternion.Euler(0, 0, 90);
				transform.localPosition = new Vector3(0, 0, initialPos.z);
				break;
			}
		}

		transform.rotation = rotation;
		Invoke("StopParticles", StopTime);
	}

	void StopParticles()
	{
		particles.Stop();
	}

	private void OnDestroy()
	{
		burst.BurstEvents -= action;
	}
}
