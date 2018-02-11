using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scripts;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerMovement : MonoBehaviour
{
	[Header("Ground")]
	public float Acceleration;
	public float BrakeAcceleration;
	public float MinSpeed;
	public float MaxSpeed;

	[Header("Air")]
	public float AirAcceleration;
	public float AirBrakeAcceleration;
	public float AirMinSpeed;
	public float AirMaxSpeed;

	[Header("Jump")]
	public float JumpForce;
	public PlayerBurst playerBurst;
	public float burstBlockTime;

	[Header("Interactions")]
	public string FloorLayerStr;
	public ContactFilter2D Contacts;
	public EnergyCarrier Energy;
	public float MaxEnergySlow;

	public delegate void MyEvent();
	public event MyEvent JumpEvents;
	private int floorLayer = -1;
	public SpriteRenderer Renderer;
	public PlayerData data;

	[HideInInspector] public float movement;
	[HideInInspector] public bool jump;

	private Collider2D collider;
	private Collider2D[] colliding = new Collider2D[20];
	private float initialGravityScale;
	internal bool grounded;
	internal bool blockedJump;
	internal Rigidbody2D rb;
	private float energySlow;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		initialGravityScale = rb.gravityScale;

		try
		{
			var colliders = GetComponents<Collider2D>();
			collider = colliders.First(d => d.enabled && d.isTrigger);
		}

		catch (Exception e)
		{
			if (collider == null)
			{
				Debug.LogWarning("Trigger collider needed for detecting ground collisions");
				Debug.Break();
			}
		}

		floorLayer = LayerMask.NameToLayer(FloorLayerStr);
		if (floorLayer == -1)
		{
			Debug.LogWarning("Floor layer name needed");
			Debug.Break();
		}

		if (Energy == null) Energy = GetComponent<EnergyCarrier>();
	}

	void Start()
	{
		Renderer.color = data.Color;
	}

	private void FixedUpdate()
	{
		grounded = false;

		energySlow = (Energy.Energy / Energy.MaxEnergy) * MaxEnergySlow;

		var count = collider.OverlapCollider(Contacts, colliding);

		for (int i =0; i<count; i++)
		{
			var colli = colliding[i];

			if (colli.gameObject == gameObject) continue;

			if (colli.gameObject.layer == floorLayer)
			{
				grounded = true;
				break;
			}
		}

		HandleJump();
		HandleMovement();
	}

	void HandleMovement()
	{
		float increase;
		float decrease;
		float xsign = Mathf.Sign(movement);
		float velsign = Mathf.Sign(rb.velocity.x);
		float minSpeed;
		float maxSpeed;
		float absx;

		if (grounded)
		{
			absx = rb.velocity.magnitude;
			rb.gravityScale = 0;
			increase = movement * Time.fixedDeltaTime * Acceleration * 10;
			decrease = Time.fixedDeltaTime * BrakeAcceleration * 10;
			minSpeed = MinSpeed - energySlow;
			maxSpeed = MaxSpeed - energySlow;
			var hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, Contacts.layerMask);
			transform.up = hit.normal;
		}

		else
		{
			absx = Mathf.Abs(rb.velocity.x);
			rb.gravityScale = initialGravityScale;
			increase = movement * Time.fixedDeltaTime * AirAcceleration * 10;
			decrease = Time.fixedDeltaTime * AirBrakeAcceleration * 10;
			minSpeed = AirMinSpeed - energySlow;
			maxSpeed = AirMaxSpeed - energySlow;
		}

		if (Mathf.Abs(movement) > .01f)
		{
			if (absx < minSpeed)
			{
				var v = transform.right.normalized * minSpeed * Mathf.Sign(movement);
				rb.velocity = new Vector2(v.x, v.y + rb.velocity.y);
			}

			else if (absx + Mathf.Abs(increase) < maxSpeed)
			{
				if (velsign == xsign)
				{
					var v = transform.right.normalized * increase;
					rb.velocity += new Vector2(v.x, v.y);
				}

				else
				{
					var v = transform.right.normalized * decrease * -velsign;
					rb.velocity += new Vector2(v.x, v.y);
				}
			}

			else
			{
				if (absx > MaxSpeed || velsign != xsign)
				{
					var v = transform.right.normalized * decrease * -velsign;
					rb.velocity += new Vector2(v.x, v.y);
				}

				else if (velsign == xsign)
				{
					var v = transform.right.normalized * maxSpeed * xsign;

					if (grounded)
					{
						rb.velocity = new Vector2(v.x, v.y);
					}

					else
					{
						rb.velocity = new Vector2(v.x, rb.velocity.y);
					}
				}
			}
		}

		else
		{
			if (Mathf.Abs(rb.velocity.x) - decrease > 0)
			{
				var v = transform.right.normalized * decrease * Mathf.Sign(rb.velocity.x);
				rb.velocity -= new Vector2(v.x, v.y);
			}

			else
			{
				rb.velocity = new Vector2(0, rb.velocity.y);
			}
		}
	}

	void HandleJump()
	{
		if (grounded && jump && !blockedJump)
		{
			if (JumpEvents != null) JumpEvents();
			playerBurst.BlockBurst();
			rb.AddForce(Vector2.up * JumpForce);
			jump = false;
		}
	}

	internal void BlockJump()
	{
		blockedJump = true;
		Invoke("UnblockJump", burstBlockTime);
	}

	internal void UnblockJump()
	{
		blockedJump = false;
	}
}
