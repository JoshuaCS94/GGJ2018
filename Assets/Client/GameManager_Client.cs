using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class GameManager_Client : MonoBehaviour
{
	private IControlHandler m_controlHandler;
	private NetworkManager m_networkManager;

	private int m_prevMovement;
	private bool m_prevJump;

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
}
