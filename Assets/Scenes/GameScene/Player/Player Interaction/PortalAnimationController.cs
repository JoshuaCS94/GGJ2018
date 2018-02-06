using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PortalAnimationController : MonoBehaviour
{
	public float rate_time = 0.5f;
	public float y_pos = 1.5f;
	public Transform child;

	private Tweener t;

	// Use this for initialization
	void Awake ()
	{
		child = transform.GetChild(0);
//		print(child);
	}

	private void Start()
	{
		t = child.DOLocalMoveY(y_pos, rate_time).SetLoops(-1,LoopType.Yoyo).Pause();
	}

	// Update is called once per frame
	void Update () {

	}

	public void StartAnimation()
	{
		print("start");
		t.Play();
	}

	public void StopAnimation()
	{
		t.Pause();
	}

	public void ChangeColor(Color c)
	{
		GetComponent<SpriteRenderer>().color = c;
		child.GetComponent<SpriteRenderer>().color = c;
	}
}
