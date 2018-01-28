using System.Linq;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainNetworkManager : NetworkManager
{
    public override void OnServerSceneChanged(string sceneName)
    {
        SceneManager.LoadScene("Level 0", LoadSceneMode.Additive);
        SceneManager.LoadScene("Level 1", LoadSceneMode.Additive);
    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        SceneManager.LoadScene("Client", LoadSceneMode.Additive);

        base.OnClientSceneChanged(conn);
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
