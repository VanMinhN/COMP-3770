using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPortal : MonoBehaviour
{

    [SerializeField]
    public string whatPortal; // what portal this is

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        GameObject player = col.gameObject;
        ThirdPersonMovement tpm = player.GetComponent<ThirdPersonMovement>();
        tpm.teleportPlayer(new Vector3(-50, 5, -15));
    }
}
