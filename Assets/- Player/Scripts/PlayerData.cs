using UnityEngine;
using UnityEngine.Networking;


public class PlayerData : NetworkBehaviour
{
	[SyncVar] public string Name;
	[SyncVar] public Color Color;
	[SyncVar] public string BodyPath;
	[SyncVar] public string CorePath;

	private void Start()
	{
		var body = Resources.Load("Robots/" + BodyPath) as GameObject;
		transform.GetChild(1).GetComponent<SpriteRenderer>().sprite =
			body.GetComponent<SpriteRenderer>().sprite;

		var core = Instantiate(Resources.Load("Cores/" + CorePath) as GameObject);
		core.transform.SetParent(transform.GetChild(1), false);
		core.GetComponent<SpriteRenderer>().material.color = Color;
	}
}
