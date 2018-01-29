﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameFollower : MonoBehaviour {

	public GameObject robot;

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
		robot = transform.parent.gameObject;
	}


	// Update is called once per frame
	void Update () {
		transform.LookAt(robot.transform);
		transform.rotation = Quaternion.identity;

	}
}
