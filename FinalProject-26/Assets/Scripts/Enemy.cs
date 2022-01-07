using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float health = 50f;

    public void TakeDamage (float amount){
        health -= amount;

        if (health <= 0f){

            Die();

        }

    }

    void Die()  // object will explode, can use as a respawn trigger???
    {
        Destroy(gameObject);
    }


}
