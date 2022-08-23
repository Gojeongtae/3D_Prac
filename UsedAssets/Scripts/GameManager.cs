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

    public FadeInOut fadeInOut;

    //플레이어 캐릭터
    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        //Variable Initializing
        Timer_PHASE = 40.0f;
        Timer_RESPAWN = 5.0f;
        time_PHASE = 0.0f;
        time_RESPAWN = 0.0f;
        Player = GameObject.FindGameObjectWithTag("Player");

        //게임 시작 전 준비
        //이벤트 트리거 오브젝트 Transform 초기화
        EvTriggerObjTransform.position = new Vector3(0f, 0f, -35.0f);
        EvTriggerObjTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        StartCoroutine("EvTriggerObj_Gen");


        //fadeInOut = GameObject.Find("FadeInOut").GetComponent<FadeInOut>();
    }

    // Update is called once per frame
    void Update()
    {
        //게임 이벤트 전부 정리
        switch (PHASE)
        {
            //********게임 시작 전********//
            case 0:
                //페이드 인
                fadeInOut.StartCoroutine(fadeInOut.FadeInStart());
                Debug.Log("a");
                break;

            //********유아기*********//
            //Case 1 = 이동 튜토리얼
            //Case 2 = 하트 파밍
            //Case 3 = 흔들기 튜토리얼
            //Case 4 = 편지 든 병 띄우기

            case 1:
                // fadeInOut = GameObject.Find("FadeInOut").GetComponent<FadeInOut>();
                // fadeInOut.StartCoroutine(fadeInOut.FadeInStart());
                // Debug.Log("a");
                //이동 튜토리얼
                //부모와의 대화 출력 위한 작업
                fadeInOut.StartCoroutine(fadeInOut.FadeInStart());
                if (FindObjectOfType<DialogueTrigger>() == null)
                {
                    DlTriggerObjTransform.position = new Vector3(0f, 0.0f, -10f);
                    DlTriggerObjTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                    StartCoroutine("DlTriggerObj_Gen");
                }
                
                //하트 파밍 이벤트로의 전환 준비
                if(FindObjectOfType<TriggerEvent>() == null)
                {
                    EvTriggerObjTransform.position = new Vector3(0f, 0f, 10.0f);
                    EvTriggerObjTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                    StartCoroutine("EvTriggerObj_Gen");
                }
                break;

            case 2: 
                // 하트 파밍
                //페이즈 타이머의 길이동안 이벤트 실행
                //이벤트 실행되는 동안 스폰 주기때마다 하트 재생성
                //두 개의 타이머를 활용, 페이즈 타이머인 40초가 되었을 때 다음 페이즈
                //리스폰 타이머는 특정 시간(여기서는 3초)마다 새로운 오브젝트를 생성
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
                    time_PHASE = 0.0f;
                    time_RESPAWN = 0.0f;
                }
                break;

            case 3:     //흔들기 튜토리얼
                break;
            case 4:     //편지 든 병 띄우기 이벤트
                break;

            //********청소년기*********//
            //Case 5 = 별 파밍
            //Case 6 = 큰 별 파밍
            //Case 7 = 독립
            case 5:     //별 파밍 이벤트
                //하트 파밍 이벤트와 동일, 파밍되는 
                if (time_PHASE <= Timer_PHASE)
                {
                    if (time_RESPAWN < Timer_RESPAWN)
                    {
                        time_RESPAWN += Time.deltaTime;
                    }
                    else
                    {
                        StartCoroutine("Star_Gen");
                        time_RESPAWN = 0.0f;
                    }
                    time_PHASE += Time.deltaTime;
                }
                else
                {
                    PHASE = 6;
                }
                break;
            case 6:     //큰 별 파밍 이벤트
                if (time_PHASE <= Timer_PHASE)
                {
                    if (time_RESPAWN < Timer_RESPAWN)
                    {
                        time_RESPAWN += Time.deltaTime;
                    }
                    else
                    {
                        StartCoroutine("Star_Gen");
                        time_RESPAWN = 0.0f;
                    }
                    time_PHASE += Time.deltaTime;
                }
                else
                {
                    PHASE = 7;
                }
                break;
            case 7:     //독립 이벤트
                break;

            //********성년기*********//
            //Case 8 = 돈 파밍
            //Case 9 = 갈매기 NPC 이벤트
            //Case 10 = 경쟁자 이벤트
            //Case 11 = 소용돌이 이벤트
            //Case 12 = 빙하 이벤트
            //Case 13 = 번개 이벤트
            //Case 14 = 거북이 NPC 이벤트
            //Case 15 = 별의 승천
            //Case 16 = 연인 이벤트 1
            //Case 17 = 연인 이벤트 2

            case 8: //돈 파밍 이벤트
                break;

            //********노년기********//
            //Case 18 = 


            default:    //아무 의미 없는 Default값 
                break;
        }
    }

    // 파밍 이벤트 시 호출하여야 할 Coroutine 목록
    // 하트, 별, 큰 별, 돈, 음표, 연인 이벤트 시 꽃 오브젝트, 백합 오브젝트 
    IEnumerator Heart_Gen()
    {
        //플레이어 Transform 불러옴
        Transform playerTransform = Player.GetComponent<Transform>();

        //범위 설정 (min, Max값)
        float min = 50.0f;
        float max = 100.0f;



        //하트 오브젝트 10개 생성
        int i = 0;
        for (i = 0; i < 10; i++)
        {
            HeartTransform.position = new Vector3(playerTransform.position.x + Random.Range(-1 * min, min), HeartTransform.position.y, playerTransform.position.z + Random.Range(min, max));
            GameObject instantHeart = Instantiate(Heart, HeartTransform.position, HeartTransform.rotation);
        }
        yield return null;
    }

    IEnumerator Star_Gen()
    {
        //플레이어 Transform 불러옴
        Transform playerTransform = Player.GetComponent<Transform>();

        //범위 설정 (min, Max값)
        float min = 50.0f;
        float max = 100.0f;

        //별 오브젝트 10개 생성
        int i = 0;
        for (i = 0; i < 10; i++)
        {
            StarTransform.position = new Vector3(playerTransform.position.x + Random.Range(-1 * min, min), StarTransform.position.y, playerTransform.position.z + Random.Range(min, max));
            GameObject instantStar = Instantiate(Star, StarTransform.position, StarTransform.rotation);
        }
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
        GameObject instantDlTrigObj = Instantiate(DialogueTriggerObject, DlTriggerObjTransform.position, DlTriggerObjTransform.rotation) ;
        Rigidbody EvTrigObjRigid = instantDlTrigObj.GetComponent<Rigidbody>();

        yield return null;
    }
}

