using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    private int right = 0;                  //randDirection ���� 0�� ���� ��� ������
    private int left = 2;                   //randDirection ���� 1�� ���� ��� ����
    private int randDirection;              //�� ���� ���� ���ϱ�(�¿�)

    private int Case3Count = 0;             //Case 3(���� Ʃ�丮��)���� �踦 ��� Ƚ��
    
    private GameObject Boat;                //����� �� ������Ʈ
    private GameManager gameManager;        //phase���� �ҷ��� GameManager ������Ʈ
    private Transform boatTransform;
    private Vector3 boatRot;                //Boat�� Transform ������Ʈ���� localRotation��(Vector3)

    private float maxSubRotNum = 20.0f;     //�ִ�� ����� ����
    private float maxTime = 2.0f;

    private int gamePhase;                  //���� �������� ���� phase

    //�� �� �𸣰����� �ϴ� �غ��� ��
    private int[] randScconds;
    private int minSecond;
    private int maxSecond;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Boat = GameObject.FindGameObjectWithTag("Boat");
    }

    public void ShakeByPhase(int phase)
    {
        boatTransform = Boat.GetComponent<Transform>();
        Vector3 boatRot = boatTransform.localRotation.eulerAngles;
        switch (phase)
        {
            case 3:
                Debug.Log(Case3Count);
                //Ű���� �Է��� Q�̱⸦ ���ϴ� ���(ù ��°, �� ��°)
                if (Case3Count == 0 || Case3Count == 2)
                {
                    if(boatRot.x >= 240.0f)
                    {
                        if(Case3Count == 0)
                        {
                            Case3Count++;

                        }
                        Debug.Log("localRotation right");
                        boatTransform.localRotation = Quaternion.Euler(new Vector3(boatRot.x + (-1) * maxSubRotNum * Time.deltaTime / maxTime, 270.0f, 0.0f));
                    }
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        Case3Count++;
                    }
                }

                //Ű���� �Է��� E�̱⸦ ���ϴ� ���(�� ��°, �� ��°)
                else if (Case3Count == 1 || Case3Count == 3)
                {
                    if(boatRot.x <= 300.0f)
                    {
                        Debug.Log("localRotation left");
                        boatTransform.localRotation = Quaternion.Euler(new Vector3(boatRot.x + maxSubRotNum * Time.deltaTime / maxTime, 270.0f, 0.0f));
                    }
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Case3Count++;
                    }
                }

                //�� ��° Ű���� �Է� ����
                else if (Case3Count == 4)
                {
                    if (boatTransform.localRotation.eulerAngles.x != 270.0f)
                    {
                        float subRot = -90.0f - boatTransform.localRotation.eulerAngles.x;
                        boatTransform.localRotation = Quaternion.Euler(new Vector3(boatRot.x + subRot * Time.deltaTime, 270.0f, 0.0f));
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
