using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;

    public Camera fpsCam; // third person camera

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // left mouse key
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);

            Enemy enemy = hit.transform.GetComponent<Enemy>();

            if (enemy != null)
            {

                enemy.TakeDamage(damage);

            }

        }
    }

}
