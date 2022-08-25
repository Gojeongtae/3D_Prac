using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public float speed;
    public float rotSpeed;
    public bool Rotis;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Rotis)
        {
            //transform.Rotate(new Vector3(0, rotSpeed * Time.deltaTime, 0));
        }
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - 2000f), speed * Time.deltaTime);
    }
}
