using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    Vector3 initPos;
    Vector3 initRot;
    float timer1 = 7.5f;
    float timer2 = 5.0f;
    Transform initTransform;
    Vector3 subPos1 = new Vector3(-25, 0, -10);
    Vector3 subPos2 = new Vector3(4, -3, 13);
    Vector3 subRot = new Vector3(0, 55, 0);
    
    float i = 0.0f;
    float j = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        initPos = new Vector3(-15, 7, 20);
        initRot = new Vector3(10, -55, 0);
        gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(initRot);
        gameObject.GetComponent<Transform>().position = initPos;
        initTransform = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (i <= timer1)
        {
            initTransform.position = new Vector3(initTransform.position.x + subPos1.x * Time.deltaTime / timer1, initTransform.position.y, initTransform.position.z + subPos1.z * Time.deltaTime / timer1);
            Vector3 FinalRotation = new Vector3(initRot.x + subRot.x * Time.deltaTime / timer1, initRot.y + subRot.y * Time.deltaTime / timer1, initRot.z + subRot.z * Time.deltaTime / timer1);
            initRot = FinalRotation;
            initTransform.rotation = Quaternion.Euler(initRot);
            i = i + Time.deltaTime;
        }
        if (i > timer1)
        {
            if (j <= timer2)
            {
                initTransform.position = new Vector3(initTransform.position.x + subPos2.x * Time.deltaTime / timer2, initTransform.position.y + subPos2.y * Time.deltaTime / timer2, initTransform.position.z + subPos2.z * Time.deltaTime / timer2);
                j = j + Time.deltaTime;
            }
        }

    }
}
