using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMoving : MonoBehaviour
{
    Vector3 initPos;
    Vector3 initRot;
    float timer1 = 10.0f;
    Transform initTransform;
    Vector3 subPos1 = new Vector3(5, 0, 20);
    Vector3 subRot = new Vector3(0, 45, 0);

    float i = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        initPos = new Vector3(-55, 0.5f, 15);
        initRot = new Vector3(0, 45, 0);
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
    }
}
