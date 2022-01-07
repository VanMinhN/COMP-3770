using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KNDamage : MonoBehaviour
{
    Collider c;
    void Start(){
        c = GetComponent<MeshCollider>();
    }
    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            PlayerStats ps = other.GetComponent<PlayerStats>();
            ps.health -= 20;
            other.GetComponent<HealthBar>().SetHealth(100 - (ps.health/ps.maxHealth)*100);
        }
        
    }
}
