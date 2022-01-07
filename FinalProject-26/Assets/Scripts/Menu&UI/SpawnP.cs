using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnP : MonoBehaviour
{
    private void Awake()
    {
        SpawnSystem.AddSpawn(transform);
    }
    private void OnDestroy()
    {
        SpawnSystem.RemoveSpawn(transform);
    }

    // This is for DEBUGGING MODE
    //Only work in editor scene, not in the game scene

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position,1f); //draw a sphere of 1 radius around the spawn point
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward*2); //draw a line on where the spawn point is facing
    }
}
