/*
    The basic networking, spawn system, and lobby networking codes are an adaption/inspiration of Diper Dino video lobby tutorial. 
    Link: youtu.be/Fx8efi2MNz0
*/
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;


public class NetworkManagerLobby : NetworkManager
{
    [SerializeField] private int minPlayers = 2; //minimum 2 players to press play button
    [Scene] [SerializeField] private string menuScene = string.Empty;

    [Header("Game Maps")]
    [SerializeField] private ListMap maps = null;

    [Header("Room")]
    [SerializeField] private NetworkRoomPlayerLobby networkRoomPlayerLobby = null;

    [Header("Game")]
    [SerializeField] private NetworkRoomGameLobby networkRoomGameLobby = null;
    [SerializeField] private GameObject playerSpawnSys = null;

    public static event Action OnClientConnected;
    public static event Action OnClientDisconnected;
    public static event Action OnServerStopped;
    public static event Action<NetworkConnection> Server_Ready;
    private MapHandler maphandler;
    public override void OnStartServer() => spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();
    public List<NetworkRoomPlayerLobby> RoomPlayers { get; } = new List<NetworkRoomPlayerLobby>();
    public List<NetworkRoomGameLobby> GamePlayers { get; } = new List<NetworkRoomGameLobby>();
    public override void OnStartClient()
    {
        var spawnablePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs");

        foreach (var prefab in spawnablePrefabs)
        {
            NetworkClient.RegisterPrefab(prefab);
        }
    }
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        OnClientConnected?.Invoke();
    }
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);

        OnClientDisconnected?.Invoke();
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        if (numPlayers >= maxConnections)
        {
            conn.Disconnect();
            return;
        }

        if (SceneManager.GetActiveScene().path != menuScene)
        {
            conn.Disconnect();
            return;
        }
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        if (SceneManager.GetActiveScene().path == menuScene)
        {
            bool isLeader = RoomPlayers.Count == 0;

            NetworkRoomPlayerLobby roomPlayerInstance = Instantiate(networkRoomPlayerLobby);

            roomPlayerInstance.IsLeader = isLeader;

            NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
            if (!roomPlayerInstance.hasAuthority)
            {
                roomPlayerInstance.gameObject.SetActive(false);
            }
        }
    }
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        if (conn.identity != null)
        {
            var player = conn.identity.GetComponent<NetworkRoomPlayerLobby>();

            RoomPlayers.Remove(player);

            NotifyPlayersOfReadyState();
        }

        base.OnServerDisconnect(conn);
    }
    public override void OnStopServer()
    {
        OnServerStopped?.Invoke();

        RoomPlayers.Clear();
        GamePlayers.Clear();
    }
    public void NotifyPlayersOfReadyState()
    {
        foreach (var player in RoomPlayers)
        {
            player.HandleReadyToStart(IsReadyToStart());
        }
    }
    private bool IsReadyToStart()
    {
        if (numPlayers < minPlayers) { return false; }

        foreach (var player in RoomPlayers)
        {
            if (!player.IsReady) { return false; }
        }

        return true;
    }
    public void StartGame()
    {
        if (SceneManager.GetActiveScene().path == menuScene)
        {
            if (!IsReadyToStart()) { return; }

            maphandler = new MapHandler(maps);
            //ServerChangeScene(maphandler.NextMap);
            ServerChangeScene("Map02");
        }
    }
    public override void ServerChangeScene(string newSceneName)
    {
        Debug.Log(". newSceneName: " + newSceneName + "Testting the Bool: " + newSceneName.StartsWith("Assets/Scenes/Map"));
        // From menu to game
        if (SceneManager.GetActiveScene().path == menuScene && newSceneName.StartsWith("Assets/Scenes/Map"))
        {
            for (int i = RoomPlayers.Count - 1; i >= 0; i--)
            {
                var conn = RoomPlayers[i].connectionToClient;
                var gameplayerInstance = Instantiate(networkRoomGameLobby);
                gameplayerInstance.SetDisplayName(RoomPlayers[i].DisplayName);
                //NetworkServer.ReplacePlayerForConnection(conn, gameplayerInstance.gameObject, true);
                NetworkServer.Destroy(conn.identity.gameObject);
                NetworkServer.ReplacePlayerForConnection(conn, gameplayerInstance.gameObject, true);
                // NetworkServer.ReplacePlayerForConnection(conn, gameplayerInstance.gameObject);
            }
        }

        base.ServerChangeScene(newSceneName);
    }
    public override void OnServerReady(NetworkConnection conn)
    {
        base.OnServerReady(conn);
        Server_Ready?.Invoke(conn);
    }
    public override void OnServerSceneChanged(string SceneName)
    {
        //When the screen is loading with scene start with this name, then start to spawn the SpawnSystem
        if (SceneName.Contains("Assets/Scenes/Map"))
        {
            GameObject PlayerInsSystem = Instantiate(playerSpawnSys);
            NetworkServer.Spawn(PlayerInsSystem);
        }
    }
}
