using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PortalAnimationController : MonoBehaviour
{

	public Transform child;

	private Tweener t;
	// Use this for initialization
	void Start ()
	{
		child = transform.GetChild(0);
		t = child.DOLocalMoveY(1.5f, 0.5f).SetLoops(-1,LoopType.Yoyo).Pause();
	}

	// Update is called once per frame
	void Update () {

	}

	public void StartAnimation()
	{
		t.Play();
	}

	public void StopAnimation()
	{
		t.Pause();
	}
}
