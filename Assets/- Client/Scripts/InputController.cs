using UnityEngine;
using UnityEngine.Networking;


public class InputController : NetworkBehaviour
{
	private IControlHandler m_controlHandler;
	private PlayerMovement m_playerMovement;
	private PlayerBurst m_playerBurst;

	private int m_prevMovement;
	private bool m_prevJump;


	private void Awake()
	{
		m_playerMovement = GetComponentInChildren<PlayerMovement>();
		m_playerBurst = GetComponentInChildren<PlayerBurst>();
	}

	public override void OnStartLocalPlayer()
	{
		m_controlHandler = GameObject.Find("Game Controller")
			#if UNITY_STANDALONE
			.AddComponent<ControlHandler_Standalone>();
			#elif UNITY_ANDROID
			.AddComponent<ControlHandler_Android>();
			#endif
	}

	private void Update()
	{
		if (!isLocalPlayer) return;

		HandleInput();
	}

	private void HandleInput()
	{
		var m = m_controlHandler.Movement;

		if (m != m_prevMovement)
		{
			Cmd_Move(m);

			m_prevMovement = m;
		}

		if (m_controlHandler.Jump)
			Cmd_Jump();

		var b = m_controlHandler.Burst;

		if (b != KeyCode.None)
			Cmd_Burst(b);
	}

	[Command]
	private void Cmd_Move(int m)
	{
		m_playerMovement.movement = m;

		Rpc_Move(m);
	}

	[ClientRpc]
	private void Rpc_Move(int m)
	{
		if (isServer) return;

		m_playerMovement.movement = m;
	}

	[Command]
	private void Cmd_Jump()
	{
		m_playerMovement.jump = true;

		Rpc_Jump();
	}

	[ClientRpc]
	private void Rpc_Jump()
	{
		if (isServer) return;

		m_playerMovement.jump = true;
	}

	[Command]
	private void Cmd_Burst(KeyCode keyCode)
	{
		m_playerBurst.Burst(keyCode);

		Rpc_Burst(keyCode);
	}

	[ClientRpc]
	private void Rpc_Burst(KeyCode keyCode)
	{
		if (isServer) return;

		m_playerBurst.Burst(keyCode);
	}
}
