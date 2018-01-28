using UnityEngine;

public class PlayerCollisionParticles : MonoBehaviour
{
	public PlayerData data;
	public PlayerMovement movement;
	public PlayerCollisions collisions;
	private ParticleSystem particles;
	public float StopTime;

	private PlayerCollisions.MyEvent action;
	private ParticleSystem.MinMaxCurve initialRate;

	private Vector3 initialPos;

	void Start ()
	{
		initialPos = transform.localPosition;
		action = PlayParticles;
		collisions.CollisionEvents += action;
		particles = GetComponent<ParticleSystem>();
		particles.Stop();
		var color = particles.main;
		color.startColor = data.playerColor;
	}

	void PlayParticles(Collision2D collision)
	{
		particles.Play();

		Quaternion rotation;

		if (collision.rigidbody.velocity.x <= 0)
		{
			rotation = Quaternion.Euler(0, 0, 180);
			transform.localPosition = -initialPos;
		}

		else
		{
			rotation = Quaternion.identity;
			transform.localPosition = initialPos;
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
		collisions.CollisionEvents -= action;
	}
}
