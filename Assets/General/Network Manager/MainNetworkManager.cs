using System.Linq;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class MainNetworkManager : NetworkManager
{
    
    public void _StartServer()
    {
        singleton.StartServer();
    }

    public void _StartClient()
    {
        singleton.StartClient();
    }

    public void _StartHost()
    {
        singleton.StartHost();
    }

    public void _StopServer()
    {
        singleton.StopServer();
    }

    public void _StopClient()
    {
        singleton.StopClient();
    }

    public void _StopHost()
    {
        singleton.StopHost();
    }

    public void _StopAll()
    {
        _StopServer();
        _StopClient();
        _StopHost();
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        base.OnServerAddPlayer(conn, playerControllerId);
    }

    public void ChangeIP(string IP)
    {
        singleton.networkAddress = GameObject.Find(IP).GetComponent<InputField>().text;
    }

    public void ChangePort(string port)
    {
        singleton.networkPort = int.Parse(GameObject.Find(port).GetComponent<InputField>().text);
    }   
}
