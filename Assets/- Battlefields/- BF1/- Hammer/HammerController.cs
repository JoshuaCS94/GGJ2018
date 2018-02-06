using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HammerController : MonoBehaviour
{

	public Animator anim;

	private int ready_state1;
	private int ready_state2;

	// Use this for initialization
	void Start () {
		 ready_state1 = Animator.StringToHash("ReadyLR");
		 ready_state2 = Animator.StringToHash("ReadyRL");
	}

	// Update is called once per frame
	void Update () {

	}

	public void Activate()
	{
		var state = anim.GetCurrentAnimatorStateInfo(0);
		if (state.shortNameHash == ready_state1 || state.shortNameHash == ready_state2)
		{
			print("meh");
			anim.SetTrigger("Active");
		}
	}


	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			Activate();

		}
	}


}
