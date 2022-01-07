using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{

    [SyncVar(hook = nameof(OnHelloCountChanged))]   // syncs updates to this variable from server to clients; OnHelloCountChanged occurs on update
    int helloCount = 0;

    private void Start()
    {

    }
/*
    void HandleMovement()
    {
        if (isLocalPlayer) 
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            Vector3 movement = new Vector3(moveHorizontal * 0.1f, 0, 0);
            //transform.position = transform.position + movement;
        }
    }
*/
    void Update()
    {
        //HandleMovement(); 

        if(isLocalPlayer && Input.GetKeyDown("x"))  // test Command call (message from clients to server)
        {
            Debug.Log("Sending Hello to Server!");
            Hello();
        }

        // if(isServer && transform.position.y > 50)   // test ClientRPC call (message from server to client)
        // {
        //     TooHigh();
        // }
    }

    public override void OnStartServer()
    {
        Debug.Log("Player has been spawned on the Server!");
    }

    [Command]    // test Command function
    void Hello()
    {
        Debug.Log("Received 'Hello' from Client!");
        helloCount += 1;    // server updates helloCount, but is updated to all clients because its a syncVar
        ReplyHello();   // since is TargetRpc, replies to the sender of the Command 'Hello()'
    }

    [TargetRpc]     // test TargetRPC function
    void ReplyHello()
    {
        Debug.Log("Received Hello from Server in reply!");
    }

    // [ClientRpc]  // test ClientRPC function
    // void TooHigh()
    // {
    //     Debug.Log("Too High!!!");
    // }

    void OnHelloCountChanged(int oldCount, int newCount)
    {
        Debug.Log($"We had {oldCount} 'Hellos' but now we have {newCount} 'Hellos'!");
    }
}
