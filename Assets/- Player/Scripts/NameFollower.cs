using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
	public class NameFollower : MonoBehaviour {

		public Transform follow;


		private Text text;
//	public Camera camera;
//	private Transform thisTransform;
//	private Vector3 roboPos;

		// Use this for initialization
		void Start ()
		{
			string name = transform.parent.GetComponentInParent<PlayerData>().playerName;
			text = GetComponentInChildren<Text>();
			text.text = name;
			text.color = transform.parent.GetComponentInParent<PlayerData>().playerColor;
			follow = transform.parent.gameObject.transform;
			TeamBase.ToggleActiveAction += OnToggleActive;
		}

		private void OnDestroy()
		{
			TeamBase.ToggleActiveAction -= OnToggleActive;
		}

		// Update is called once per frame
		void Update () {
			transform.LookAt(follow);
			transform.rotation = Quaternion.identity;

		}

		void OnToggleActive(GameObject o)
		{
			if (o == transform.parent.gameObject)
			{
				text.enabled = !text.enabled;
			}
		}
	}
}
