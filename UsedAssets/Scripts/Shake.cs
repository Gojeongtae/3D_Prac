using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    private int right = 0;                  //randDirection 값이 0이 나올 경우 오른쪽
    private int left = 2;                   //randDirection 값이 1이 나올 경우 왼쪽
    private int randDirection;              //배 기울기 방향 정하기(좌우)

    private int Case3Count = 0;             //Case 3(흔들기 튜토리얼)에서 배를 흔든 횟수

    private GameObject Boat;                //기울일 배 오브젝트
    private GameManager gameManager;        //phase값을 불러올 GameManager 오브젝트
    private Transform boatTransform;
    private Vector3 boatRot;                //Boat의 Transform 컴포넌트에서 localRotation값(Vector3)

    private float maxSubRotNum;     //최대로 기울일 정도
    private float maxTime = 2.0f;

    private int gamePhase;                  //현재 진행중인 게임 phase

    //해본다
    private float rightAngle = 240.0f;
    private float leftAngle = 300.0f;
    private float x = 270.0f;



    //될 지 모르겠지만 일단 해보는 것
    private int[] randScconds;
    private int minSecond;
    private int maxSecond;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void ShakeByPhase(int phase)
    {
        switch (phase)
        {
            case 3:
                //키보드 입력이 Q이기를 원하는 경우(첫 번째, 세 번째)
                if (Case3Count == 0 || Case3Count == 2)
                {
                    maxSubRotNum = rightAngle - x;
                    if (x >= 240.0f)
                    {
                        x += maxSubRotNum * Time.deltaTime / maxTime;
                        transform.localRotation = Quaternion.Euler(new Vector3(x, 270.0f, 0.0f));
                    }
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        gameManager.GenerateDialogue();
                        Case3Count++;
                    }
                }

                //키보드 입력이 E이기를 원하는 경우(두 번째, 네 번째)
                if (Case3Count == 1 || Case3Count == 3)
                {
                    maxSubRotNum = leftAngle - x;
                    if (x < 300.0f)
                    {
                        x += maxSubRotNum * Time.deltaTime / maxTime;
                        transform.localRotation = Quaternion.Euler(new Vector3(x, 270, 0.0f));
                    }
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        gameManager.GenerateDialogue();
                        Case3Count++;
                    }
                }

                //네 번째 키보드 입력 이후
                if (Case3Count == 4)
                {
                    if (x != 270.0f)
                    {
                        transform.localRotation = Quaternion.Euler(new Vector3(270.0f, 270.0f, 0.0f));
                        Case3Count++;
                    }
                }
                break;

            case 13:

                break;

            default:

                break;
        }
    }

    public int getCase3Count()
    {
        return Case3Count;
    }

}
