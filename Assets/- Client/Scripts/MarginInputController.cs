using UnityEngine;
using UnityEngine.Networking;

public class MarginInputController : NetworkBehaviour
{
	public PlayerMovement Movement;
	public float BurstTimeout = 1;
	public float PercentMargin = 10;

	private int X = 0;
	private int Y = 0;
	private int prevX = 0;
	private int prevY = 0;

	private IControlHandler _handler;
	private float _rightLastPressedTime, _leftLastPressedTime, _upLastPressedTime = -1;
	private bool _rightFree, _leftFree, _upFree;

	public override void OnStartLocalPlayer()
	{
		_handler = GameObject.Find("Game Controller")
		#if UNITY_STANDALONE
					.AddComponent<ControlHandler_Standalone>();
		#elif UNITY_ANDROID
			.AddComponent<ControlHandler_Android>();
			#endif
	}

	void Update ()
	{
		if (!isLocalPlayer) return;

		var margin = Screen.width * PercentMargin / 100f;
		X = 0;
		Y = 0;
		bool foundRight = false;
		bool foundLeft = false;
		bool foundUp = false;

		foreach (var touch in Input.touches)
		{
			if (touch.position.x < margin)
			{
				foundLeft = true;
				X--;
			}

			else if (touch.position.x > Screen.width - margin)
			{
				foundRight = true;
				X++;
			}

			else
			{
				foundUp = true;
				Y++;
			}
		}

		if (foundLeft)
		{
			if (_leftFree)
			{
				if (Time.time - _leftLastPressedTime < BurstTimeout)
				{
					CmdBurst(KeyCode.LeftArrow);
				}
				_leftFree = false;
				_leftLastPressedTime = Time.time;
			}
		}
		else
		{
			_leftFree = true;
		}

		if (foundRight)
		{
			if (_rightFree)
			{
				if (Time.time - _rightLastPressedTime < BurstTimeout)
				{
					CmdBurst(KeyCode.RightArrow);
				}
				_rightFree = false;
				_rightLastPressedTime = Time.time;
			}
		}
		else
		{
			_rightFree = true;
		}

		if (foundUp)
		{
			if (_upFree)
			{
				if (Time.time - _upLastPressedTime < BurstTimeout)
				{
					CmdBurst(KeyCode.UpArrow);
				}
				_upFree = false;
				_upLastPressedTime = Time.time;
			}
		}
		else
		{
			_upFree = true;
		}


		var newX = X > 0 ? 1 :
			X < 0 ? -1 : 0;
		var newY = Y > 0 ? 1 :
			Y < 0 ? -1 : 0;

		if (newX != prevX)
		{
			prevX = newX;
			CmdMove(newX);
		}

		if (newY != prevY)
		{
			prevY = newY;
			CmdJump(newY != 0);
		}
	}

	[Command]
	void CmdMove(float movement)
	{
		Movement.movement = (int)movement;
	}

	[Command]
	void CmdJump(bool jump)
	{
		Movement.jump = jump;
	}

	[Command]
	void CmdBurst(KeyCode direction)
	{
		Movement.playerBurst.Burst(direction);
	}
}
