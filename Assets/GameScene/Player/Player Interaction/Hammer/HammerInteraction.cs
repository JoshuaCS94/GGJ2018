using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerInteraction : MonoBehaviour
{
	public LayerMask Affected;

	private Collider2D Right;

	private Collider2D Left;

	public PortalAnimationController p_animation;

	public HammerController ham_con;

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
		var rb = other.gameObject.GetComponent<Rigidbody2D>();
		if (Left.IsTouchingLayers(Affected))
		{
			print("left");
			var mypos = Left.bounds.center;
			var playerPos = other.gameObject.transform.position;
			var direction = Vector3.Normalize(mypos - playerPos);

			print(direction);

			rb.AddForce(direction*-300 + Vector3.left*1000);
		}
		else
		{
			var mypos = Right.bounds.center;
//			var contact = other.contacts[points / 2].point;
			var playerPos = other.bounds.center;
			var direction = Vector3.Normalize(mypos - playerPos);

			print(direction);

			rb.AddForce(direction*-300 + Vector3.right*1000);
		}
	}


	public void On()
	{
		p_animation.StartAnimation();
	}

	public void Off()
	{
		p_animation.StopAnimation();
	}
}
