using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerStats : MonoBehaviour
{
    public bool redTeam;
    public Vector3 spawnPoint;
    public float health;
    public float maxHealth;
    public float strength;
    public float attackSpeed; // in attacks per second
    public string playerClass;
    private GameObject KAOE;
    
    // Start is called before the first frame update
    void Start()
    {
        redTeam = true;

        if(redTeam){
            spawnPoint = new Vector3(-55f, 2f, 30f);
        }
        else{
            spawnPoint= new Vector3(-14, 2f, -3f);
        }

        if (playerClass == "Knight")
        {
            maxHealth = 200f;
            health = maxHealth;
            strength = 30f;
            attackSpeed = 1f; 
        }
        else if (playerClass == "Berserker")
        {
            maxHealth = 125f;
            health = maxHealth;
            strength = 40f;
            attackSpeed = 1f; 
        }
        else if (playerClass == "Fighter")
        {
            maxHealth = 150f;
            health = maxHealth;
            strength = 30f;
            attackSpeed = 2f; 
        }
        else if (playerClass == "Assassin")
        {
            maxHealth = 100f;
            health = maxHealth;
            strength = 40f;
            attackSpeed = 2.5f; 
        }
        else
        {
            Debug.Log("Class Defaulted");
            maxHealth = 1000f;
            health = maxHealth;
            strength = 1000f;
            attackSpeed = 3f; 
        }

        
    }
    // Update is called once per frame
    void Update()
    {    
    }
}
