using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWeakness : MonoBehaviour
{
	private Animator anim;

	public
	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {

	}


	private void OnTriggerEnter2D(Collider other)
	{
		GetComponent<EnergyCarrier>().Energy = 0;
		anim.SetBool("Die", true);

	}

	public void DeadCalllBack()
	{

	}

}
