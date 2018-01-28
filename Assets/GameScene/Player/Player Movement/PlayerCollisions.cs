using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
	public string PlayerLayerStr;
	public float ForceMultiplier;
	public PlayerMovement Movement;
	public EnergyCarrier Energy;
	public float MaxProbabilityToDie;

	private int PlayerLayer;

	public virtual void Awake()
	{
		PlayerLayer = LayerMask.NameToLayer(PlayerLayerStr);
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.layer != PlayerLayer) return;

		Movement.rb.AddForce(other.relativeVelocity * ForceMultiplier);

		var probability = MaxProbabilityToDie * (Energy.Energy / Energy.MaxEnergy);
		var random = Random.value;
		if (random < probability)
		{

		}
	}
}
