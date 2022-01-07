using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ThirdPersonMovement : NetworkBehaviour
{
    public CharacterController controller;
    public Transform cam;
    private bool groundedPlayer;
    private bool airial = false;
    private float gravityValue = -9.81f * 1.5f;
    public float speed = 6f;
    private Vector3 playerVelocity;
    float prevy;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    float angle;
    bool isMap01 = false;
    private Rigidbody playerRB;
    public GameObject KAOE, BAOE, PAOE, RAOE;
    float targetAngle;
    public bool redTeam;

    private void Start()
    {
        redTeam = false;
        playerRS();
        
        playerRB = GetComponent<Rigidbody>();
        playerVelocity.y = -2f;
        prevy = transform.position.y;
    }
    public void playerRS(){
        
        if(redTeam){
            controller.enabled =false;
            controller.transform.position = new Vector3(-55f, 2f, 10f);
            controller.enabled =true;
        }
        else{
            controller.enabled =false;
            controller.transform.position = new Vector3(-32f, 2f, -29f);
            controller.enabled =true;
            Debug.Log(controller.transform.position);
        }
    }

    void HandleMovement()
    {
        if (isLocalPlayer)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
            Vector3 layerVelocity = new Vector3(horizontal, 0f, vertical).normalized;

            if(direction.magnitude >= 0.1f)
            {
                targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
            
        }
        
    }

    public void teleportPlayer(Vector3 tpPosition)
    {
        controller.enabled = false;
        controller.transform.position = tpPosition;
        controller.enabled = true;
        Debug.Log("TELEPORTED!");
    }

    IEnumerator waiter(GameObject newObj)
    {
        //Wait for 4 seconds
        yield return new WaitForSeconds(0.2f);
        Destroy(newObj);
    }
    void KnightAttack(Vector3 pos, Quaternion id){
        //Vector3 k = pos.normalized*id;
        GameObject newObj = Instantiate(KAOE, pos + new Vector3(0f,0f,1f), id);
        newObj.transform.parent = gameObject.transform;
        newObj.transform.localPosition = new Vector3(1f, 0, 3f);
        StartCoroutine(waiter(newObj));
    }
    void BerserkerAttack(Vector3 pos, Quaternion id){
        GameObject newObj = Instantiate(BAOE, pos + new Vector3(0f,0f,1f), id);
        newObj.transform.parent = gameObject.transform;
        newObj.transform.localPosition = new Vector3(-1f, 0, 2f);
        StartCoroutine(waiter(newObj));
    }
    void RangerAttack(Vector3 pos, Quaternion id){
        GameObject newObj = Instantiate(RAOE, pos + new Vector3(0f,0f,1f), id);
        newObj.transform.parent = gameObject.transform;
        newObj.transform.localPosition = new Vector3(-1f, 0, 1f);
        StartCoroutine(waiter(newObj));
    }
    void PolarmAttack(Vector3 pos, Quaternion id){
        GameObject newObj = Instantiate(PAOE, pos + new Vector3(0f,0f,0f), id);
        newObj.transform.parent = gameObject.transform;
        newObj.transform.localPosition = new Vector3(-3f, 0, -3f);
        StartCoroutine(waiter(newObj));
    }

    public override void OnStartClient()
    {
        if (isLocalPlayer){
            cam = GameObject.Find("Main Camera").GetComponent<Transform>();
        }
    } 
    
    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {

            Cursor.visible=false;
            Cursor.lockState = CursorLockMode.Locked;
            //Overly complicated jump function
            controller.Move(playerVelocity * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            if(Input.GetMouseButtonDown(0)){
                Debug.Log("Click");
                KnightAttack(transform.position,Quaternion.Euler(0f, angle, 0f));
            }


            HandleMovement();
            playerVelocity.y += gravityValue * Time.deltaTime;
            if(controller.isGrounded){
                playerVelocity.y = -2f;
                groundedPlayer =true;
                Debug.Log("Grounded is TRUE!!!!");
            }
            if(groundedPlayer){
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    playerVelocity.y = 7f;
                    groundedPlayer = false;
                    Debug.Log("Grounded is FALSE!!!!");
                }
            }

            if(gameObject.GetComponent<PlayerStats>().health == 0){
                playerRS();
                gameObject.GetComponent<PlayerStats>().health=gameObject.GetComponent<PlayerStats>().maxHealth;
                gameObject.GetComponent<HealthBar>().SetHealth(0);
            }
            prevy = transform.position.y;
            }
    }
}
