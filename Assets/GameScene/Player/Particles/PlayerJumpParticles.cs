using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpParticles : MonoBehaviour
{
	public PlayerMovement movement;
	private ParticleSystem particles;
	public float StopTime;

	private PlayerMovement.JumpEvent action;

	void Start ()
	{
		action = PlayParticles;
		movement.JumpEvents += action;
		particles = GetComponent<ParticleSystem>();
		particles.Stop();
		var color = particles.main;
		color.startColor = Color.red;
	}

	void PlayParticles()
	{
		particles.Play();
		Invoke("StopParticles", StopTime);
	}

	void StopParticles()
	{
		particles.Stop();
	}

	private void OnDestroy()
	{
		movement.JumpEvents -= action;
	}
}
