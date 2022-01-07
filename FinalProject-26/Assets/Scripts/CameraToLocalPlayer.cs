using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Cinemachine;
using UnityEngine.SceneManagement;

public class CameraToLocalPlayer : NetworkBehaviour
{


    [SerializeField] private CinemachineFreeLook freeLookCameraToLocalPlayer;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        if (isLocalPlayer)
        {
            freeLookCameraToLocalPlayer = CinemachineFreeLook.FindObjectOfType<CinemachineFreeLook>();
            freeLookCameraToLocalPlayer.LookAt = this.gameObject.transform;
            freeLookCameraToLocalPlayer.Follow = this.gameObject.transform;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
    }
}
