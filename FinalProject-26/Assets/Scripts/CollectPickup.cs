using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPickup : MonoBehaviour
{

    [SerializeField]
    public string whatPickup;   // TEMP, just allows editor to set what pickup something is prob delete later

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
        if(col.tag != "Player"){
            return;
        }
        gameObject.SetActive(false);
        if(whatPickup == "Health"){
            col.GetComponent<PlayerStats>().health += 100;
        }
        else if(whatPickup == "Weapon"){
            Debug.Log("");
        }
        else if(whatPickup == "Armor"){
            Debug.Log("");
        }
    }
}
