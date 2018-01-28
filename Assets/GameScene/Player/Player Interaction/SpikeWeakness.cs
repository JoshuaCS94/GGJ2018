using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWeakness : MonoBehaviour
{
	private Animator anim;

	public TeamBase Team;
	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {

	}


	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer != LayerMask.NameToLayer("TouchDanger"))
			return;
		var pm = GetComponent<PlayerMovement>();
		var rb = GetComponent<Rigidbody2D>();

		pm.enabled = false;
		rb.isKinematic = true;
		rb.velocity = Vector3.zero;
		GetComponent<EnergyCarrier>().Energy = 0;
//		anim.SetBool("Die", true);
		DeadCallBack();
	}

	public void DeadCallBack()
	{
		Team.SpawnPlayer(gameObject.GetComponent<TeamMember>());
	}

}
