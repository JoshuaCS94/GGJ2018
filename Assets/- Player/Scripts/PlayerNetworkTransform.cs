using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerNetworkTransform : NetworkBehaviour
{
	[Range(1, 50)]
	public float UpdateSendRate;
	[Range(1, 50)]
	public float PackageDelay;
	[Range(.1f, 1)]
	public float InterpolationAmmount;

	PlayerMovement movement;

	private Vector3 _position;
	private Coroutine _sendPositionCoroutine;

	void Awake()
	{
		movement = GetComponentInChildren<PlayerMovement>();
	}

	void Start()
	{
		if (isServer)
		{
			_sendPositionCoroutine = StartCoroutine(SendPositionCoroutine());
		}

		else
		{
			movement.rb.simulated = false;
		}
	}

	private void OnDestroy()
	{
		if(isServer) StopCoroutine(_sendPositionCoroutine);
	}

	[ClientRpc]
	void RpcChangePosition(Vector3 pos)
	{
		_position = pos;
	}

	void Update ()
	{
		if (!isServer)
		{
			movement.transform.position = Vector3.Lerp(movement.transform.position, _position, InterpolationAmmount);
		}
	}

	IEnumerator SendPositionCoroutine()
	{
		while (true)
		{
			var offset = movement.rb.velocity / PackageDelay;
			RpcChangePosition(movement.transform.position + new Vector3(offset.x, offset.y, 0));
			yield return new WaitForSeconds(1/UpdateSendRate);
		}
	}
}
