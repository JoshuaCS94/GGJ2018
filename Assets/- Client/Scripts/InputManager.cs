using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;


internal enum GameMsgType
{
	Movement = 1000,
	Jump,
	Burst
}

internal class BurstMessage : MessageBase
{
	public KeyCode keyCode;
}

public class InputManager : MonoBehaviour
{
	private IControlHandler m_controlHandler;
	private NetworkManager m_networkManager;

	private int m_prevMovement;
	private bool m_prevJump;

	// Use this for initialization
	private void Start ()
	{
		if (NetworkServer.active)
		{
			NetworkServer.RegisterHandler((short)GameMsgType.Movement, MovementHandler);
			NetworkServer.RegisterHandler((short)GameMsgType.Jump, JumpHandler);
			NetworkServer.RegisterHandler((short)GameMsgType.Burst, BurstHandler);

			return;
		}

		m_networkManager = GameObject.Find("Network Manager").GetComponent<NetworkManager>();

		m_controlHandler = GameObject.Find("Game Controller")
			#if UNITY_STANDALONE
			.AddComponent<ControlHandler_Standalone>();
			#elif UNITY_ANDROID
			.AddComponent<ControlHandler_Android>();
			#endif
	}

	#region Client side

	// Update is called once per frame
	private void Update()
	{
		if (NetworkServer.active) return;

		HandleInput();
	}

	private void HandleInput()
	{
		var m = m_controlHandler.Movement;

		if (m != m_prevMovement)
		{
			SendMovement(m);

			m_prevMovement = m;
		}

		if (m_controlHandler.Jump)
			SendJump();

		var b = m_controlHandler.Burst;

		if (b != KeyCode.None)
			SendBurst(b);
	}

	private void SendMovement(int m)
	{
		m_networkManager.client.Send((short) GameMsgType.Movement,
			new IntegerMessage(m));
	}

	private void SendJump()
	{
		m_networkManager.client.Send((short) GameMsgType.Jump,
			new EmptyMessage());
	}

	private void SendBurst(KeyCode keyCode)
	{
		m_networkManager.client.Send((short) GameMsgType.Burst,
			new BurstMessage {keyCode = keyCode});
	}

	#endregion

	#region Server side

	private void MovementHandler(NetworkMessage netMsg)
	{
		var movMsg = netMsg.ReadMessage<IntegerMessage>();

		var player = netMsg.conn.playerControllers[0].gameObject.GetComponentInChildren<PlayerMovement>();

		player.movement = movMsg.value;
	}

	private void JumpHandler(NetworkMessage netMsg)
	{
		var player = netMsg.conn.playerControllers[0].gameObject.GetComponentInChildren<PlayerMovement>();

		player.jump = true;
	}

	private void BurstHandler(NetworkMessage netMsg)
	{
		var burstMsg = netMsg.ReadMessage<BurstMessage>();

		var player = netMsg.conn.playerControllers[0].gameObject.GetComponentInChildren<PlayerBurst>();

		player.Burst(burstMsg.keyCode);
	}

	#endregion
}
