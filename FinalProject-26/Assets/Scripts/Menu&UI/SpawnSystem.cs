using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
public class SpawnSystem : NetworkBehaviour
{
    [SerializeField] private GameObject playerobject = null;

    private static List<Transform> points = new List<Transform>();
    private int index = 0; //index of each spawn for each player

    public static void AddSpawn(Transform transform)
    {
        points.Add(transform);
        points = points.OrderBy(x=>x.GetSiblingIndex()).ToList(); //make sure the order of the spawn is correct
    }
    public static void RemoveSpawn(Transform transform)
    {
        points.Remove(transform);
    }
    public override void OnStartServer()
    {
        NetworkManagerLobby.Server_Ready += spawnPlayer;
    }

    [ServerCallback]

    private void OnDestroy()
    {
        NetworkManagerLobby.Server_Ready -= spawnPlayer;
    }

    [Server]
    public void spawnPlayer(NetworkConnection conn)
    {
        Transform spawnPoint = points.ElementAtOrDefault(index);
        if (spawnPoint == null)
        {
            Debug.LogError($"Error on this index {index}");
            return;
        }
        GameObject playerIns = Instantiate(playerobject,points[index].position, points[index].rotation);
       /* if (index == 0 || index%2 == 0) {
            playerIns.gameObject.tag = "Red";
        } else { playerIns.gameObject.tag = "Blue"; }*/
        //spawn the player on the other objects. Conn is included to send authority.
        NetworkServer.Spawn(playerIns,conn);
       // NetworkServer.AddPlayerForConnection(conn, playerIns);
        index++;
    }
}

