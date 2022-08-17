using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMeetIsland : MonoBehaviour
{
    Transform initTransform;                        //���� �� ������Ʈ�� Transform ������Ʈ

    Vector3 initPos = new Vector3(-20, 0, -25);     // ���� �� ������ Position ���̰�
    Vector3 initRot = new Vector3(90, 45, 0);       // ���� �� ������ Rotation ���̰�
    
    Vector3 subPos = new Vector3(10.5f, 0, 17);     // ���� ���� �� �谡 �����̴� Position ���̰�
    Vector3 subRot = new Vector3(0, 30, 0);         // ���� ���� �� �谡 �����̴� Rotation ���̰�

    //Ÿ�̸� ����
    float Timer = 5.0f;
    float i = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject island = GameObject.FindGameObjectWithTag("Island");
        Vector3 islandPos = island.GetComponent<Transform>().position;
        Vector3 islandRot = island.GetComponent<Transform>().rotation.eulerAngles;

        gameObject.GetComponent<Transform>().position = islandPos + initPos;                        //Position ���̰��� �����༭ ������ ���۵� �� ���� ��ġ�� ����� ��
        gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(islandRot + initRot);      //Rotation ���̰��� �����༭ ������ ���۵� �� ���� ��ġ�� ����� ��

        initTransform = gameObject.GetComponent<Transform>();                                       //���� initTransform�� ���� ���� Transform �� �־��ֱ�
    }

    // Update is called once per frame
    void Update()
    {
        if (i <= Timer)
        {
            //Position�� ���
            initTransform.position = new Vector3(initTransform.position.x + subPos.x * Time.deltaTime / Timer, initTransform.position.y, initTransform.position.z + subPos.z * Time.deltaTime / Timer);


            //Rotation�� ���
            Vector3 FinalRotation = initTransform.rotation.eulerAngles;
            FinalRotation = new Vector3(FinalRotation.x + subRot.x * Time.deltaTime / Timer, FinalRotation.y + subRot.y * Time.deltaTime / Timer, FinalRotation.z + subRot.z * Time.deltaTime / Timer);
            initTransform.rotation = Quaternion.Euler(FinalRotation);
            i = i + Time.deltaTime;
        }
    }
}
