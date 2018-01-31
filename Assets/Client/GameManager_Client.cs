﻿using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class GameManager_Client : MonoBehaviour
{
	private IControlHandler m_controlHandler;
	private NetworkManager m_networkManager;

	// Use this for initialization
	private void Start ()
	{
		m_networkManager = GameObject.Find("Network Manager").GetComponent<NetworkManager>();

		m_controlHandler = GameObject.Find("Game Controller")
			#if UNITY_STANDALONE
			.AddComponent<ControlHandler_Standalone>();
			#elif UNITY_ANDROID
			.AddComponent<ControlHandler_Android>();
			#endif
	}

	// Update is called once per frame
	private void Update()
	{
		ManageInput();
	}

	private void ManageInput()
	{
		var m = m_controlHandler.Movement;

		if (!Mathf.Approximately(m.x, 0) || !Mathf.Approximately(m.y, 0))
			SendMovement(m.x, m.y);
		else
			SendFinishedMovement();

		var b = m_controlHandler.Burst;

		if (b != KeyCode.None)
			SendBurst(b);
	}

	private void SendMovement(float x, float y)
	{
		m_networkManager.client.Send((short) GameMsgType.Movement,
			new MovementMessage {delta = new Vector2(x, y)});
	}

	private void SendFinishedMovement()
	{
		m_networkManager.client.Send((short) GameMsgType.FinishedMovement,
			new EmptyMessage());
	}

	private void SendBurst(KeyCode keyCode)
	{
		m_networkManager.client.Send((short) GameMsgType.Burst,
			new BurstMessage {keyCode = keyCode});
	}
}
