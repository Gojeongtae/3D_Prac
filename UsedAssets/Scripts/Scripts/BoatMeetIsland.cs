using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMeetIsland : MonoBehaviour
{
    Transform initTransform;                        //현재 배 오브젝트의 Transform 컴포넌트

    Vector3 initPos = new Vector3(-20, 0, -25);     // 섬과 배 사이의 Position 차이값
    Vector3 initRot = new Vector3(90, 45, 0);       // 섬과 배 사이의 Rotation 차이값
    
    Vector3 subPos = new Vector3(10.5f, 0, 17);     // 연출 실행 시 배가 움직이는 Position 차이값
    Vector3 subRot = new Vector3(0, 30, 0);         // 연출 실행 시 배가 움직이는 Rotation 차이값

    //타이머 관련
    float Timer = 5.0f;
    float i = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject island = GameObject.FindGameObjectWithTag("Island");
        Vector3 islandPos = island.GetComponent<Transform>().position;
        Vector3 islandRot = island.GetComponent<Transform>().rotation.eulerAngles;

        gameObject.GetComponent<Transform>().position = islandPos + initPos;                        //Position 차이값을 더해줘서 연출이 시작될 때 배의 위치를 계산해 줌
        gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(islandRot + initRot);      //Rotation 차이값을 더해줘서 연출이 시작될 때 배의 위치를 계산해 줌

        initTransform = gameObject.GetComponent<Transform>();                                       //현재 initTransform에 현재 배의 Transform 값 넣어주기
    }

    // Update is called once per frame
    void Update()
    {
        if (i <= Timer)
        {
            //Position값 계산
            initTransform.position = new Vector3(initTransform.position.x + subPos.x * Time.deltaTime / Timer, initTransform.position.y, initTransform.position.z + subPos.z * Time.deltaTime / Timer);


            //Rotation값 계산
            Vector3 FinalRotation = initTransform.rotation.eulerAngles;
            FinalRotation = new Vector3(FinalRotation.x + subRot.x * Time.deltaTime / Timer, FinalRotation.y + subRot.y * Time.deltaTime / Timer, FinalRotation.z + subRot.z * Time.deltaTime / Timer);
            initTransform.rotation = Quaternion.Euler(FinalRotation);
            i = i + Time.deltaTime;
        }
    }
}
