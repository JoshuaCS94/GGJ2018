using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class PlayerCreationMsg : MessageBase
{
    public string name;
    public string bodyBase;
    public string bodyCore;
    public Color color;
}


public class MainNetworkManager : NetworkManager
{
    public GarageManager garage;

    public override void OnServerSceneChanged(string sceneName)
    {
        SceneManager.LoadScene("Level 0", LoadSceneMode.Additive);
        SceneManager.LoadScene("Level 1", LoadSceneMode.Additive);
    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        SceneManager.LoadScene("Client", LoadSceneMode.Additive);

        ClientScene.Ready(conn);

        if (!autoCreatePlayer)
            return;

        ClientScene.AddPlayer(conn, 0, new PlayerCreationMsg
        {
            name = garage.PlayerName,
            bodyBase = garage.CurrentBodyName,
            bodyCore = garage.CurrentCoreName,
            color = garage.Color
        });
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        var playerMsg = extraMessageReader.ReadMessage<PlayerCreationMsg>();
        print("Hello: " + playerMsg.name + " of color " + playerMsg.color);
        base.OnServerAddPlayer(conn, playerControllerId, extraMessageReader);
    }

    public new void StartClient()
    {
        base.StartClient();
    }

    public new void StartServer()
    {
        base.StartServer();
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        base.OnServerAddPlayer(conn, playerControllerId);
    }

    public void ChangeIP(string IP)
    {
        networkAddress = IP;
    }

    public void ChangePort(string port)
    {
        networkPort = int.Parse(port);
    }
}
