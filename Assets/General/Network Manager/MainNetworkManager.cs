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
    public LobbyManager lobbyManager;

    private int m_currentTeam = 0;

    public override void OnServerSceneChanged(string sceneName)
    {
        SceneManager.LoadScene("Level 0", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Game");

    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        SceneManager.LoadSceneAsync("Client");

        ClientScene.Ready(conn);

        if (!autoCreatePlayer)
            return;

        ClientScene.AddPlayer(conn, 0, new PlayerCreationMsg
        {
            name = lobbyManager.PlayerName,
            bodyBase = lobbyManager.CurrentBodyName,
            bodyCore = lobbyManager.CurrentCoreName,
            color = lobbyManager.Color
        });
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        if (!playerPrefab)
        {
            if (!LogFilter.logError)
                return;
            Debug.LogError("The PlayerPrefab is empty on the NetworkManager. Please setup a PlayerPrefab object.");
        }
        else if (!playerPrefab.GetComponent<NetworkIdentity>())
        {
            if (!LogFilter.logError)
                return;
            Debug.LogError("The PlayerPrefab does not have a NetworkIdentity. Please add a NetworkIdentity to the player prefab.");
        }
        else if (playerControllerId < conn.playerControllers.Count &&
                 conn.playerControllers[playerControllerId].IsValid &&
                 conn.playerControllers[playerControllerId].gameObject != null)
        {
            if (!LogFilter.logError)
                return;
            Debug.LogError("There is already a player at that playerControllerId for this connections.");
        }
        else
        {
            var startPosition = GetStartPosition();
            var playerMsg = extraMessageReader.ReadMessage<PlayerCreationMsg>();

            var player = !(startPosition != null)
                ? Instantiate(playerPrefab, playerPrefab.transform.position, Quaternion.identity)
                : Instantiate(playerPrefab, startPosition.position, startPosition.rotation);

            var body = Resources.Load("Robots/" + playerMsg.bodyBase) as GameObject;
            player.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite =
                body.GetComponent<SpriteRenderer>().sprite;

            var core = Instantiate(Resources.Load("Cores/" + playerMsg.bodyCore) as GameObject);
            core.transform.SetParent(player.transform.GetChild(1), false);
            core.GetComponent<SpriteRenderer>().material.color = playerMsg.color;

            var playerData = player.GetComponent<PlayerData>();
            playerData.name = playerMsg.name;
            playerData.playerColor = playerMsg.color;

            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

            // Add players
            var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            print(gameManager);

            var team1 = gameManager.team1;
            var team2 = gameManager.team2;

            if (m_currentTeam % 2 == 0)
            {
                team1.AddPlayer(player);
            }
            else
            {
                team2.AddPlayer(player);
            }

            m_currentTeam++;
        }
    }

    public new void StartClient()
    {
        base.StartClient();
    }

    public new void StartServer()
    {
        base.StartServer();
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
