using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager_Client : MonoBehaviour {

	private NetworkManager m_networkManager;

	// Use this for initialization
	void Start ()
	{
		m_networkManager = GameObject.Find("Network Manager").GetComponent<NetworkManager>();
	}

	// Update is called once per frame
	void Update()
	{
		var mx = Input.GetAxisRaw("Horizontal");
		var my = Input.GetAxisRaw("Vertical");

		if (!Mathf.Approximately(mx, 0) || !Mathf.Approximately(my, 0))
			SendMovement(mx, my);

		if (Input.GetKeyDown(KeyCode.UpArrow))
			SendBurst(KeyCode.UpArrow);
		else if (Input.GetKeyDown(KeyCode.RightArrow))
			SendBurst(KeyCode.RightArrow);
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
			SendBurst(KeyCode.LeftArrow);
		else if (Input.GetKeyDown(KeyCode.DownArrow))
			SendBurst(KeyCode.DownArrow);
	}

	private void SendMovement(float x, float y)
	{
		m_networkManager.client.Send((short) GameMsgType.Movement,
			new MovementMessage {delta = new Vector2(x, y)});
	}

	private void SendBurst(KeyCode keyCode)
	{
		m_networkManager.client.Send((short) GameMsgType.Burst,
			new BurstMessage {keyCode = keyCode});
	}
}
