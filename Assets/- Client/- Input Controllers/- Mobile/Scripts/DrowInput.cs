using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class DrowInput : MonoBehaviour, IControlHandler {

	public float Movement { get; private set; }
	public bool Jump { get; private set; }
	public KeyCode Burst { get; set; }

	[Header("Percent")]
	public float XMinDistancePercent = 0.07f;
	public float XMaxDisplacementPercent = 0.01f;
	public float YMaxDisplacementPercent = 0.2f;
	public float SensitiveEpsilonPercent = 0.005f;

	[Header("ScreenDistance")]
	public float XMinDistance;
	public float XMaxDisplacement;
	public float YMaxDisplacement;
	public float SensitiveEpsilon;

	public float _width;
	public float _height;

	[Header("Movement")]
	public float _xMov;

	public float _yMov;

	private KeyCode _lastDir;

	private int _movTouchId;

	private Vector2 _referencePoint;

	private int[] _tapCount;

	private float _actualXMov;

	private float _actualYMov;



	// Use this for initialization
	void Start ()
	{
		_width = Screen.width;
		_height = Screen.height;

		_xMov = 0;
		_yMov = 0;
		_movTouchId = int.MaxValue;
		_actualXMov = 0;
		_actualYMov = 0;
		_referencePoint = Vector3.zero;
		Burst = KeyCode.None;
		_lastDir = KeyCode.UpArrow;
		Movement = 0;
		Jump = false;
		_tapCount = new int[5];
	}


	// Update is called once per frame
	private void Update()
	{
		XMinDistance = XMinDistancePercent * _width;
		XMaxDisplacement = XMaxDisplacementPercent * _width;
		SensitiveEpsilon = SensitiveEpsilonPercent * _width;
		YMaxDisplacement = YMaxDisplacementPercent * _height;

		Burst = KeyCode.None;

		foreach (var touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				_referencePoint = touch.position;
				//tener en cuenta al burst
			}
			if (touch.phase == TouchPhase.Moved)
			{
				var xdif = touch.position.x - _referencePoint.x;
				var ydif = touch.position.y - _referencePoint.y;

				_yMov = ydif / YMaxDisplacement;

				if (Mathf.Abs(xdif) < SensitiveEpsilon )
					return;

				if (touch.fingerId == _movTouchId)
				{
					_lastDir = xdif > 0 ? KeyCode.RightArrow : KeyCode.LeftArrow;
					if (Mathf.Abs(xdif) < XMinDistance)
					{
						_referencePoint = touch.position;
						_xMov = xdif / XMaxDisplacement;

					}
					else
					{
						_xMov = xdif / XMaxDisplacement;
					}
				}
				else
				{
					_movTouchId = touch.fingerId;
					_referencePoint = touch.position;
				}
			}
			if (touch.phase == TouchPhase.Stationary)
			{
//				_referencePoint = touch.position;
//				print("stationary");
			}
			if (touch.phase == TouchPhase.Ended)
			{
				_xMov = 0;
				_yMov = 0;
			}

			Burst = touch.tapCount > _tapCount[touch.fingerId] ? _lastDir : KeyCode.None;
//			if (Burst != KeyCode.None)
//			{
//				_tapCount[touch.fingerId] = 0;
//			}
//			else
				_tapCount[touch.fingerId] = touch.tapCount;
		}

		if (_xMov > 1)
			_xMov = 1;
		else if (_xMov < -1)
			_xMov = -1;

		Movement = _xMov;
		Jump = _yMov >= 0.2f;
		if (Burst != KeyCode.None)
		{
			if (Jump)
			{
				Burst = KeyCode.UpArrow;
			}
			else if (_yMov <= -0.2f)
			{
				Burst = KeyCode.DownArrow;
			}
		}

		print(Burst);
	}
}
