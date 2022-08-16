using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    Vector3 destination = new Vector3(3, 3, 3);

    void Update()
    {
        
        transform.position =
            Vector3.MoveTowards(transform.position, destination, 1);
        

        Vector3 speed = Vector3.zero; // (0,0,0) 은 .zero 로도 표현가능
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref speed, 0.1f);
    }

}