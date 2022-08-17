using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    Transform initTransform;                        //���� ī�޶��� Transform ������Ʈ

    Vector3 initPos = new Vector3(11, 1, 25);     //��� ī�޶� ������ localPosition ���̰�
    Vector3 initRot = new Vector3(0, -120, 0);      //��� ī�޶� ������ localRotation ���̰�


    Vector3 subPos = new Vector3(-1, 5.5f, -21);  //���� ���� �� ī�޶� �����̴� localPosition ���̰�
    Vector3 subRot = new Vector3(30, 35, 0);       //���� ���� �� ī�޶� �����̴� localRotation ���̰�

    //Ÿ�̸� ����
    float Timer = 5.0f;
    float i = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Transform>().localPosition = initPos;
        gameObject.GetComponent<Transform>().localRotation = Quaternion.Euler(initRot);
        initTransform = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (i <= Timer)
        {
            initTransform.localPosition = new Vector3(initTransform.localPosition.x + subPos.x * Time.deltaTime / Timer, initTransform.localPosition.y + subPos.y * Time.deltaTime / Timer, initTransform.localPosition.z + subPos.z * Time.deltaTime / Timer);
            
            Vector3 FinalRotation = initTransform.localRotation.eulerAngles;
            FinalRotation = new Vector3(FinalRotation.x + subRot.x * Time.deltaTime / Timer, FinalRotation.y + subRot.y * Time.deltaTime / Timer, FinalRotation.z + subRot.z * Time.deltaTime / Timer);
            initTransform.localRotation = Quaternion.Euler(FinalRotation);
            
            i = i + Time.deltaTime;
        }
    }
}
