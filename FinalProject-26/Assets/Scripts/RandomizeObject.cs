using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeObject : MonoBehaviour
{
    public GameObject myPrefab;
    public GameObject prefab2;
    public GameObject prefab3;
    public GameObject prefab4;

    // Start is called before the first frame update
    void Start()
    {
        float random = Random.value;
        if(random <= 0.25f){
            Instantiate(myPrefab, transform.position, Quaternion.identity);
        }
        else if(random <= 0.5f){
            Instantiate(prefab2, transform.position, Quaternion.identity);
        }
        else if(random <= 0.75f){
            Instantiate(prefab3, transform.position, Quaternion.identity);
        }
        else if(random <= 0.8f){
            Instantiate(prefab4, transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject);
    }
}