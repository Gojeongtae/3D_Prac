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

    private float maxSubRotNum;     //�ִ�� ����� ����
    private float maxTime = 2.0f;

    private int gamePhase;                  //���� �������� ���� phase

    //�غ���
    private float rightAngle = 240.0f;
    private float leftAngle = 300.0f;
    private float x = 270.0f;



    //�� �� �𸣰����� �ϴ� �غ��� ��
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
                //Ű���� �Է��� Q�̱⸦ ���ϴ� ���(ù ��°, �� ��°)
                if (Case3Count == 0 || Case3Count == 2)
                {
                    maxSubRotNum = rightAngle - x;
                    if(x >= 240.0f)
                    {
                        x += maxSubRotNum * Time.deltaTime / maxTime;
                        transform.localRotation = Quaternion.Euler(new Vector3(x, 270.0f, 0.0f));
                    }
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        Case3Count++;
                    }
                }

                //Ű���� �Է��� E�̱⸦ ���ϴ� ���(�� ��°, �� ��°)
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
                        Case3Count++;
                    }
                }

                //�� ��° Ű���� �Է� ����
                if (Case3Count == 4)
                {
                    if (boatTransform.localRotation.eulerAngles.x != 270.0f)
                    {
                        float subRot = -90.0f - transform.localRotation.eulerAngles.x;
                        transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x + subRot * Time.deltaTime, 270.0f, 0.0f));
                        Debug.Log("3");
                    }
                }
                break;
            
            case 13:

                Debug.Log("4");
                break;

            default:

                Debug.Log("5");
                break;
        }
    }

    public int getCase3Count()
    {
        Debug.Log("6");
        return Case3Count;
    }

}