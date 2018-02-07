using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerInteraction : MonoBehaviour
{
	public LayerMask Affected;

	public float forceMultipliyer = 1200;

	private Collider2D Right;

	private Collider2D Left;

	private bool contacting;

	public PortalAnimationController p_animation;

	// Use this for initialization
	void Awake ()
	{
		var colliders = GetComponents<BoxCollider2D>();
		Left = colliders[0];
		Right = colliders[1];
	}

	void Start()
	{
		Off();
	}
	// Update is called once per frame
	void Update () {

	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (contacting)
		{

			var rb = other.gameObject.GetComponent<Rigidbody2D>();
			if (Left.IsTouchingLayers(Affected))
			{
				var mypos = Left.bounds.center;
				var playerPos = other.gameObject.transform.position;
				var direction = Vector3.Normalize(mypos - playerPos);


				rb.AddForce(direction * -300 + Vector3.left * forceMultipliyer);
			}
			else if (Right.IsTouchingLayers(Affected))
			{
				var mypos = Right.bounds.center;
//			var contact = other.contacts[points / 2].point;
				var playerPos = other.bounds.center;
				var direction = Vector3.Normalize(mypos - playerPos);


				rb.AddForce(direction * -300 + Vector3.right * forceMultipliyer);
			}
		}
	}


	public void On()
	{
		contacting = false;
		p_animation.StartAnimation();
	}

	public void Off()
	{
		p_animation.StopAnimation();
	}

	public void StartMovement()
	{
		contacting = true;
	}
}
