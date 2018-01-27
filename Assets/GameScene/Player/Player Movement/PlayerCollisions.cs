using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
	public string PlayerLayerStr;
	public float ForceMultiplier;
	public PlayerMovement movement;
	
	private int PlayerLayer;

	public virtual void Awake()
	{
		PlayerLayer = LayerMask.NameToLayer(PlayerLayerStr);
	}

//	public virtual void OnTriggerEnter2D(Collider2D other)
//	{
//		if (other.gameObject.layer == PlayerLayer)
//		{
//			if (other.gameObject != gameObject && !other.isTrigger)
//			{
//				Destroy(other.transform.parent.gameObject);
//			}
//		}
//	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.layer != PlayerLayer) return;
		
		movement.rb.AddForce(other.relativeVelocity * ForceMultiplier);
		
//		var vel = movement.rb.velocity;
//		var ovel = other.rigidbody.velocity;
//		if (Mathf.Sign(ovel.x) ==
//		    Mathf.Sign(vel.x))
//		{
//			if (vel.x <= ovel.x)
//			{
//				movement.rb.AddForce((ovel - vel) * ForceMultiplier);
//			}
//		}
//
//		else
//		{
//			var absvel = Mathf.Abs(vel.x);
//			var absovel = Mathf.Abs(ovel.x);
//			if (absvel <= absovel)
//			{
//				movement.rb.AddForce(new Vector2(absovel - absvel, vel.y) * ForceMultiplier);
//			}
//		}
	}
}
