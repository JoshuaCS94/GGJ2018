using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;


internal enum GameMsgType
{
	Movement = 1000, Jump, Burst
}


internal class BurstMessage : MessageBase
{
	public KeyCode keyCode;
}


public class GameManager_Server : MonoBehaviour {

	// Use this for initialization
	private void Start()
	{
		NetworkServer.RegisterHandler((short)GameMsgType.Movement, MovementHandler);
		NetworkServer.RegisterHandler((short)GameMsgType.Jump, JumpHandler);
		NetworkServer.RegisterHandler((short)GameMsgType.Burst, BurstHandler);
	}

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
}
