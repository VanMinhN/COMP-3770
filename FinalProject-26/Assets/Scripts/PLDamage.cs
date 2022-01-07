using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLDamage : MonoBehaviour
{
    Collider c;
    void Start(){
        c = GetComponent<MeshCollider>();
    }
    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            PlayerStats ps = other.GetComponent<PlayerStats>();
            ps.health -= 10;
            other.GetComponent<HealthBar>().SetHealth(100 - (ps.health/ps.maxHealth)*100);
        }
        
    }
}
