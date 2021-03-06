﻿using System.Collections;
using UnityEngine;

namespace Scripts
{
	public class EnergyCarrier : MonoBehaviour
	{

		public float Energy = 0;

		public float MaxEnergy = 50;
		public float EnergyAddedPerSecond = 5;

		public float EnergyUpdateTime = 0.5f;



		// Use this for initialization
		void Start () {


		}

		// Update is called once per frame
		void Update () {

		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if(other.gameObject.layer == LayerMask.NameToLayer("Energy"))
				StartCoroutine("AddEnergy");
			if (other.gameObject.layer == LayerMask.NameToLayer("Base"))
			{
				other.gameObject.GetComponentInParent<TeamBase>().DischargeEnergy(this);
			}

		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if(other.gameObject.layer == LayerMask.NameToLayer("Energy"))
				StopCoroutine("AddEnergy");
		}


		IEnumerator AddEnergy()
		{
			while (true)
			{
				Energy += EnergyAddedPerSecond * EnergyUpdateTime;
				if (Energy >= MaxEnergy)
				{
					Energy = MaxEnergy;
					yield break;
				}
				yield return new WaitForSeconds(EnergyUpdateTime);
			}

		}
	}
}
