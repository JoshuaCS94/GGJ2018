using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


internal enum GameMsgType
{
	Movement, Burst
}

internal class MovementMessage : MessageBase
{
	public Vector2 delta;
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
		NetworkServer.RegisterHandler((short)GameMsgType.Burst, BurstHandler);
	}

	private void MovementHandler(NetworkMessage netMsg)
	{
		var movMsg = netMsg.ReadMessage<MovementMessage>();

		var player = netMsg.conn.playerControllers[0].gameObject.GetComponent<PlayerMovement>();

		player.x = movMsg.delta.x;
		player.y = movMsg.delta.y;
	}

	private void BurstHandler(NetworkMessage netMsg)
	{
		var burstMsg = netMsg.ReadMessage<BurstMessage>();

		var player = netMsg.conn.playerControllers[0].gameObject.GetComponent<PlayerBurst>();

		player.Burst(burstMsg.keyCode);
	}
}
