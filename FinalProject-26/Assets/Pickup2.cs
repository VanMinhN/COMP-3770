using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Pickup2 : MonoBehaviour
{
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }
    float speed = 3f;
    float height = 0.2f;
   
    // Update is called once per frame
    void Update()
    {
        float newY = Mathf.Sin(Time.time * speed) * height;
        transform.position = new Vector3(pos.x, pos.y + newY + 0.3f, pos.z);
    }
}
