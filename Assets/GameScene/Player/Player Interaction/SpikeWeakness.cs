using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWeakness : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}


	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.layer == LayerMask.NameToLayer("Energy"))
			StartCoroutine("AddEnergy");

	}

	private void OnTriggerExit(Collider other)
	{
		if(other.gameObject.layer == LayerMask.NameToLayer("Energy"))
			StopCoroutine("AddEnergy");
	}
}
