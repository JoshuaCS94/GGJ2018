using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementParticles : MonoBehaviour
{
	public PlayerMovement movement;
	private ParticleSystem particles;

	void Start ()
	{
		particles = GetComponent<ParticleSystem>();
		var color = particles.main;
		color.startColor = Color.red;
	}

	void Update ()
	{
		if (!movement.grounded) particles.Stop();
		else particles.Play();
	}
}
