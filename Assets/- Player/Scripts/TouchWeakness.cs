﻿using UnityEngine;

namespace Scripts
{
	public class TouchWeakness : MonoBehaviour
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
			var pb = GetComponent<PlayerBurst>();
			var rb = GetComponent<Rigidbody2D>();

			pm.enabled = false;
			pb.enabled = false;
			rb.isKinematic = true;
			rb.velocity = Vector2.zero;
			GetComponent<EnergyCarrier>().Energy = 0;
//		anim.SetBool("Die", true);
			DeadCallBack();
		}

		public void DeadCallBack()
		{
			Team.KillPlayer(gameObject.GetComponent<TeamMember>());
		}

	}
}
