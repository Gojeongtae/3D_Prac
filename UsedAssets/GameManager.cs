using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager: MonoBehaviour
{
    //페이즈 및 재생성 관련 타이머 및 수치
    public int PHASE = 0;
    private float Timer_PHASE;
    private float Timer_RESPAWN;
    private float time_PHASE;
    private float time_RESPAWN;

    //파밍 이벤트 시 생성해야 할 GameObject
    public GameObject Heart;                // 하트 오브젝트
    public GameObject Star;                 // 별 오브젝트
    public GameObject LStar;                // 큰 별 오브젝트
    public GameObject Money;                // 돈 오브젝트
    public GameObject Note;                 // 음표 오브젝트
    public GameObject Flower;               // 연인 이벤트 시 꽃 오브젝트
    public GameObject Lily;                 // 백합 오브젝트

    //갈매기와의 이벤트 시 생성해야 할 GameObject
    public GameObject Enemy;                // 경쟁자 오브젝트
    public GameObject Whirlpool;            // 소용돌이 오브젝트
    public GameObject Iceberg;              // 빙하 오브젝트
    public GameObject Iceberg_Wall;         // 빙하벽 오브젝트
    public GameObject Thunder;              // 번개 오브젝트

    //그 외 이벤트 시 생성해야 할 GameObject
    public GameObject EventTriggerObject;   // 이벤트 작동시키는 빈 오브젝트
    public GameObject DialogueTriggerObject;// 대화 작동시기키는 빈 오브젝트
    public GameObject BottleWithDiary;      // 일기 들어간 병 오브젝트
    public GameObject Boat_Big;             // 큰 배 오브젝트
    public GameObject Island;               // 섬 오브젝트
    public GameObject Rock;                 // 돌 오브젝트

    //파밍 이벤트 시 생성되는 GameObject의 Transform Component
    public Transform HeartTransform;        // 하트 오브젝트의 Transform
    public Transform StarTransform;         // 별 오브젝트의 Transform
    public Transform LStarTransforml;       // 큰 별 오브젝트의 Transform
    public Transform MoneyTransform;        // 돈 오브젝트의 Transform
    public Transform NoteTransform;         // 음표 오브젝트의 Transform
    public Transform FlowerTransform;       // 꽃 오브젝트의 Transform
    public Transform LilyTransform;         // 백합 오브젝트의 Transform

    //갈매기 이벤트 시 생성되는 GameObject의 Transform Component
    public Transform EnemyTransform;        // 경쟁자 오브젝트의 Transform
    public Transform WhirlpoolTransform;    // 소용돌이 오브젝트의 Transform
    public Transform IcebergTransform;      // 빙하 오브젝트의 Transform
    public Transform Iceberg_WallTransform; // 빙하벽 오브젝트의 Transform
    public Transform ThunderTransform;      // 번개 오브젝트의 Transform

    //그 외 이벤트 시 생성되는 GameObject의 Transform Component
    public Transform EvTriggerObjTransform;
    public Transform DlTriggerObjTransform;
    public Transform BottleWDTransform;
    public Transform Boat_BigTransform;
    public Transform IslandTransform;
    public Transform RockTransform;

    //플레이어 캐릭터
    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        //Variable Initializing
        Timer_PHASE = 40.0f;
        Timer_RESPAWN = 3f;
        time_PHASE = 0.0f;
        time_RESPAWN = 0.0f;

        //게임 시작 전 준비
        //이벤트 트리거 오브젝트 Transform 초기화
        EvTriggerObjTransform.position = new Vector3(0f, 0f, -40.0f);
        EvTriggerObjTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        StartCoroutine("EvTriggerObj_Gen");
    }

    // Update is called once per frame
    void Update()
    {
        //게임 이벤트 전부 정리
        switch (PHASE)
        {
            //********게임 시작 전********//
            case 0:
                break;

            //********유아기*********//
            //Case 1 = 이동 튜토리얼
            //Case 2 = 하트 파밍
            //Case 3 = 흔들기 튜토리얼
            //Case 4 = 편지 든 병 띄우기

            case 1: //이동 튜토리얼
                //부모와의 대화 출력 위한 작업
                DlTriggerObjTransform.position = new Vector3(0f, 0.0f, 0f);
                DlTriggerObjTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                StartCoroutine("DlTriggerObj_Gen");
                
                //하트 파밍 이벤트로의 전환 준비
                EvTriggerObjTransform.position = new Vector3(0f, 40.0f, 0f);
                EvTriggerObjTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                StartCoroutine("EvTriggerObj_Gen");

                break;

            case 2: // 하트 파밍
                //페이즈 타이머의 길이동안 이벤트 실행
                //이벤트 실행되는 동안 스폰 주기때마다 하트 재생성
                if (time_PHASE <= Timer_PHASE)
                {
                    if (time_RESPAWN < Timer_RESPAWN)
                    {
                        time_RESPAWN += Time.deltaTime;
                    }
                    else
                    {
                        StartCoroutine("Heart_Gen");
                        time_RESPAWN = 0.0f;
                    }
                    time_PHASE += Time.deltaTime;
                }
                else
                {
                    PHASE = 3;
                }
                break;

            case 3:
                //흔들기 튜토리얼
                //일단 후순위 구현

                break;
            case 4:
                //편지 든 병 띄우기 이벤트


                break;

            //********성년기*********//
            //Case 5 = 별 파밍
            //Case 6 = 큰 별 파밍
            //Case 7 = 독립
            //case 5: break;
            //case 6: break;
            //default: break;
        }
    }

    // 파밍 이벤트 시 호출하여야 할 Coroutine 목록
    // 하트, 별, 큰 별, 돈, 음표, 연인 이벤트 시 꽃 오브젝트, 백합 오브젝트 
    IEnumerator Heart_Gen()
    {
        GameObject instantHeart = Instantiate(Heart, HeartTransform.position, HeartTransform.rotation);
        //Rigidbody bulletRigid = instantHeart.GetComponent<Rigidbody>();
        //bulletRigid.velocity = HeartTransform.forward * 5;



        yield return null;
    }

    IEnumerator Star_Gen()
    {
        GameObject instantStar = Instantiate(Star, StarTransform.position, StarTransform.rotation);
        Rigidbody starRigid = instantStar.GetComponent<Rigidbody>();
        starRigid.velocity = StarTransform.forward * 5;

        yield return null;
    }

    IEnumerator LStar_Gen()
    {
        GameObject instantLStar = Instantiate(LStar, LStarTransforml.position, LStarTransforml.rotation);
        Rigidbody lstarRigid = instantLStar.GetComponent<Rigidbody>();
        lstarRigid.velocity = LStarTransforml.forward * 5;

        yield return null;
    }

    IEnumerator Money_Gen()
    {
        GameObject instantMoney = Instantiate(Money, MoneyTransform.position, MoneyTransform.rotation);
        Rigidbody moneyRigid = instantMoney.GetComponent<Rigidbody>();
        moneyRigid.velocity = MoneyTransform.forward * 5;

        yield return null;
    }

    IEnumerator Note_Gen()
    {
        GameObject instantNote = Instantiate(Note, NoteTransform.position, NoteTransform.rotation);
        Rigidbody noteRigid = instantNote.GetComponent<Rigidbody>();
        noteRigid.velocity = NoteTransform.forward * 5;

        yield return null;
    }

    IEnumerator Flower_Gen()
    {
        GameObject instantFlower = Instantiate(Flower, FlowerTransform.position, FlowerTransform.rotation);
        Rigidbody flowerRigid = instantFlower.GetComponent<Rigidbody>();
        flowerRigid.velocity = FlowerTransform.forward * 5;

        yield return null;
    }

    IEnumerator Lily_Gen()
    {
        GameObject instantLily = Instantiate(Lily, LilyTransform.position, LilyTransform.rotation);
        Rigidbody lilyRigid = instantLily.GetComponent<Rigidbody>();
        lilyRigid.velocity = LilyTransform.forward * 5;

        yield return null;
    }

    // 갈매기와의 이벤트(시련) 시 호출하여야 할 Coroutine 목록
    // 경쟁자, 소용돌이, 빙하, 빙하벽, 번개
    IEnumerator Enemy_Gen()
    {
        GameObject instantEnemy = Instantiate(Enemy, EnemyTransform.position, EnemyTransform.rotation);
        Rigidbody enemyRigid = instantEnemy.GetComponent<Rigidbody>();
        enemyRigid.velocity = EnemyTransform.forward * 5;

        yield return null;
    }

    IEnumerator Whirlpool_Gen()
    {
        GameObject instantWhirlpool = Instantiate(Whirlpool, WhirlpoolTransform.position, WhirlpoolTransform.rotation);
        Rigidbody whirlpoolRigid = instantWhirlpool.GetComponent<Rigidbody>();
        whirlpoolRigid.velocity = WhirlpoolTransform.forward * 5;

        yield return null;
    }

    IEnumerator Iceberg_Gen()
    {
        GameObject instantIceberg = Instantiate(Iceberg, IcebergTransform.position, IcebergTransform.rotation);
        Rigidbody icebergRigid = instantIceberg.GetComponent<Rigidbody>();
        icebergRigid.velocity = IcebergTransform.forward * 5;

        yield return null;
    }

    IEnumerator Iceberg_Wall_Gen()
    {
        GameObject instantIceberg_Wall = Instantiate(Iceberg_Wall, Iceberg_WallTransform.position, Iceberg_WallTransform.rotation);
        Rigidbody iceberg_WallRigid = instantIceberg_Wall.GetComponent<Rigidbody>();
        iceberg_WallRigid.velocity = Iceberg_WallTransform.forward * 5;

        yield return null;
    }

    IEnumerator Thunder_Gen()
    {
        GameObject instantThunder = Instantiate(Thunder, ThunderTransform.position, ThunderTransform.rotation);
        Rigidbody thunderRigid = instantThunder.GetComponent<Rigidbody>();
        thunderRigid.velocity = ThunderTransform.forward * 5;

        yield return null;
    }

    //그 외 이벤트 시 호출하여야 할 Coroutine 목록
    //일기 담긴 병, 큰 배, 섬, 빈 장면 대화 트리거,
    IEnumerator EvTriggerObj_Gen()
    {
        GameObject instantEvTrigObj = Instantiate(EventTriggerObject, EvTriggerObjTransform.position, EvTriggerObjTransform.rotation);
        Rigidbody EvTrigObjRigid = instantEvTrigObj.GetComponent<Rigidbody>();

        yield return null;
    }
    IEnumerator DlTriggerObj_Gen()
    {
        GameObject instantEvTrigObj = Instantiate(DialogueTriggerObject, DlTriggerObjTransform.position, DlTriggerObjTransform.rotation);
        Rigidbody DlTrigObjRigid = instantEvTrigObj.GetComponent<Rigidbody>();

        yield return null;
    }
}

