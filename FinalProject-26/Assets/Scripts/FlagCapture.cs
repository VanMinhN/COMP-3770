using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagCapture : MonoBehaviour
{

    [SerializeField]
    public string whatTeam; // what team does this flag belong to
    public GameObject flag;
    public Collider carrier;
    private Vector3 ogPos;
    private Quaternion ogRot;

    bool isCarried;

    // Start is called before the first frame update
    void Start()
    {
        ogPos = gameObject.transform.position;
        ogRot = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(isCarried){
            if(carrier.GetComponent<PlayerStats>().health == 0){
                gameObject.transform.parent = null;
                gameObject.transform.rotation = ogRot;
                gameObject.transform.position = ogPos;
                isCarried =false;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        carrier = col;
        if(col.tag == "Player"){
            isCarried=true;
            gameObject.transform.parent = col.gameObject.transform;
            Debug.Log("Attach");
            gameObject.transform.localPosition = new Vector3(1f, 1, -1f);
        }
        if(col.tag == "Red" && gameObject.tag == "Blue"){
            isCarried=true;
            gameObject.transform.parent = null;
            gameObject.transform.rotation = ogRot;
            gameObject.transform.position = ogPos;
            GameObject scoreTracker = GameObject.Find("ScoreTracker");
            ScoreTracker st = scoreTracker.GetComponent<ScoreTracker>();
            st.redScored();

            RespawnPickups();
        }
        if(col.tag == "Blue" && gameObject.tag == "Red"){
            isCarried=true;
            gameObject.transform.parent = null;
            gameObject.transform.rotation = ogRot;
            gameObject.transform.position = ogPos;
            GameObject scoreTracker = GameObject.Find("ScoreTracker");
            ScoreTracker st = scoreTracker.GetComponent<ScoreTracker>();
            st.blueScored();

            RespawnPickups();
        }
    }
    void RespawnPickups(){
        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag ("Pickup");
        foreach(GameObject go in gameObjectArray){
                go.SetActive(true);
        }
    }
}
