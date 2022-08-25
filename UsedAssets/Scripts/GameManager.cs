using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class GameManager : MonoBehaviour
{
    // **************************************************************************** Event Variables ************************************************************************************ //
    //When The Farming Event GameObject
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
    public Transform LStarTransform;       // 큰 별 오브젝트의 Transform
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


    // ************************************************************************************* GamePlay Variables ********************************************************************************** //

    //플레이어 캐릭터
    private GameObject Player;
    private GameObject Boat;

    //페이즈 및 재생성 관련 타이머 및 수치
    private int PHASE = 0;
    private float Timer_PHASE;
    private float Timer_RESPAWN;
    private float time_PHASE;
    private float time_RESPAWN;

    public FadeInOut fadeInOut;

    //페이즈별 Dialogues의 페이즈, 인덱스 계산
    private Dialogues dialogs;
    private int dialogIndex = 0;


    //페이즈별 오브젝트 등장 개수 제한
    private int LStar_Count = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Variable Initializing
        Timer_PHASE = 40.0f;
        Timer_RESPAWN = 5.0f;
        time_PHASE = 0.0f;
        time_RESPAWN = 0.0f;

        Player = GameObject.FindGameObjectWithTag("Player");
        Boat = GameObject.FindGameObjectWithTag("Boat");

        //Read
        ReadJson();
    }

    // Update is called once per frame
    void Update()
    {
        Transform playerTransform = Player.GetComponent<Transform>();

        //게임 이벤트 전부 정리
        switch (PHASE)
        {
            //******************************************** 게임 시작 전 ********************************************//
            //******************************************************************************************************//
            case 0:
                if (fadeInOut != null)
                    fadeInOut.StartCoroutine(fadeInOut.FadeIn(2));

                if (FindObjectOfType<TriggerEvent>() == null)
                {
                    EvTriggerObjTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z + 10.0f);
                    EvTriggerObjTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                    StartCoroutine("EvTriggerObj_Gen");
                }
                break;

            //*********************************************** 유아기 ***********************************************//
            //******************************************************************************************************//
            //이동 튜토리얼 -> 하트 파밍    -> 흔들기 튜토리얼 -> 편지 든 병 띄우기
            

            case 1:     //클릭 이동 튜토리얼

                //부모와의 대화 출력 위한 작업
                if (FindObjectOfType<DialogueTrigger>() == null)
                {
                    Dialogue dialog = new Dialogue();
                    //대화 내용 추가
                    DlTriggerObjTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z + 20.0f);
                    DlTriggerObjTransform.rotation = Quaternion.Euler(Vector3.zero);
                    dialog = chooseDialog();

                    if(dialog.sentences != null) 
                    {
                        StartCoroutine("DlTriggerObj_Gen", dialog);
                    }
                }
                
                //하트 파밍 이벤트로의 전환 준비
                if (FindObjectOfType<TriggerEvent>() == null && FindObjectOfType<DialogueTrigger>() == null)
                {
                    EvTriggerObjTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z + 15.0f);
                    EvTriggerObjTransform.rotation = Quaternion.Euler(Vector3.zero);
                    StartCoroutine("EvTriggerObj_Gen");
                }
                break;

            case 2:     // 하트 파밍 이벤트

                //페이즈 타이머의 길이동안 이벤트 실행
                if (time_PHASE <= Timer_PHASE)
                {
                    //이벤트 실행되는 동안 스폰 주기마다 하트 재생성
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
                //다음 페이즈로의 이동
                else
                {
                    PHASE = 3;
                    time_PHASE = 0.0f;
                    time_RESPAWN = 0.0f;
                }
                break;

            case 3:     //흔들기 튜토리얼

                //흔들기 페이즈 길이 (60초)
                Timer_PHASE = 199999999.0f;
                
                if (time_PHASE <= Timer_PHASE)
                {
                    //흔들기 튜토리얼 시작 후 10초 뒤 대화 시작
                    if (time_PHASE == 0.0f)
                    {
                        Dialogue dialog = new Dialogue();

                        DlTriggerObjTransform.position = Player.GetComponent<Transform>().position;
                        DlTriggerObjTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                        dialog = chooseDialog();

                        if (dialog.sentences != null)
                        {
                            StartCoroutine("DlTriggerObj_Gen", dialog);
                        }
                    }
                    //흔들기 튜토리얼 실행
                    if(time_PHASE >= 2.0f)
                    {
                        if(Boat.GetComponent<Shake>().getCase3Count() <= 5)
                        {
                            Boat.GetComponent<Shake>().ShakeByPhase(PHASE);
                        }
                    }
                    time_PHASE += Time.deltaTime;
                }
                else
                {
                    //다음 페이즈로의 이동
                    PHASE = 4;

                    //타이머 변수 초기화
                    time_PHASE = 0.0f;
                    time_RESPAWN = 0.0f;
                    Timer_PHASE = 0.0f;
                    Timer_RESPAWN = 0.0f;
                }
                break;

            case 4:     //편지 든 병 띄우기 이벤트
                Timer_PHASE = 60.0f;
                Timer_RESPAWN = 10.0f;
                //편지 띄우기 전 대화
                if (FindObjectOfType<DialogueTrigger>() == null)
                {
                    DlTriggerObjTransform.position = new Vector3(0f, 0.0f, -10f);
                    DlTriggerObjTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                    StartCoroutine("DlTriggerObj_Gen");
                }


                //편지 띄운 후 대화
                if (FindObjectOfType<DialogueTrigger>() == null)
                {
                    DlTriggerObjTransform.position = new Vector3(0f, 0.0f, -10f);
                    DlTriggerObjTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                    StartCoroutine("DlTriggerObj_Gen");
                }
                //하트 파밍 이벤트로의 전환 준비
                if (FindObjectOfType<TriggerEvent>() == null)
                {
                    EvTriggerObjTransform.position = new Vector3(0f, 0f, 10.0f);
                    EvTriggerObjTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                    StartCoroutine("EvTriggerObj_Gen");
                }
                break;

            //********************************************** 청소년기 **********************************************//
            //******************************************************************************************************//
            //노 젓기 튜토리얼 -> 별 파밍 이벤트 -> 큰 별 파밍 이벤트 -> 독립 이벤트
            
            case 5:     //노 젓기 (클릭-드래그 이동) 튜토리얼
                break;

            case 6:     //별 파밍 이벤트
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

            case 7:     //큰 별 파밍 이벤트

                if (time_PHASE <= Timer_PHASE && time_PHASE < 11.0f)
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
                if(time_PHASE < 20.0f && time_PHASE > 11.0f)
                {
                    if(LStar_Count == 0)
                    {
                        StartCoroutine("LStar_Gen");
                        LStar_Count++;
                    }
                    time_PHASE += Time.deltaTime;
                }
                else
                {
                    PHASE = 7;
                }
                break;

            case 8:     //독립 이벤트
                //아들과 부모와의 대화
                if (FindObjectOfType<DialogueTrigger>() == null)
                {
                    DlTriggerObjTransform.position = new Vector3(0f, 0.0f, -10f);
                    DlTriggerObjTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                    StartCoroutine("DlTriggerObj_Gen");
                }

                //하트 파밍 이벤트로의 전환 준비
                if (FindObjectOfType<TriggerEvent>() == null)
                {
                    EvTriggerObjTransform.position = new Vector3(0f, 0f, 10.0f);
                    EvTriggerObjTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                    StartCoroutine("EvTriggerObj_Gen");
                }
                break;

            //******************************************** 성년기 **************************************************//
            //******************************************************************************************************//
            //돈 파밍 이벤트  -> 갈매기 NPC 이벤트  -> 경쟁자 이벤트      -> 소용돌이 이벤트     -> 빙하 이벤트   -> 
            //번개 이벤트     -> 거북이 NPC 이벤트  -> 별의 승천 이벤트   -> 연인 이벤트 1(만남) -> 연인 이벤트 2(프로포즈)

            case 9:     //돈 파밍 이벤트
                break;

            case 10:    //갈매기 NPC 이벤트
                break;

            case 11:    //경쟁자 이벤트
                break;

            case 12:    //소용돌이 이벤트
                if (time_PHASE <= Timer_PHASE)
                {
                    if (time_RESPAWN < Timer_RESPAWN)
                    {
                        time_RESPAWN += Time.deltaTime;
                    }
                    else
                    {
                        StartCoroutine("Whirlpool_Gen");
                        time_RESPAWN = 0.0f;
                    }
                    time_PHASE += Time.deltaTime;
                }
                else
                {
                    PHASE = 13;
                }
                break;

            case 13:    //빙하 이벤트
                if (time_PHASE <= Timer_PHASE)
                {
                    if (time_RESPAWN < Timer_RESPAWN)
                    {
                        time_RESPAWN += Time.deltaTime;
                    }
                    else
                    {
                        StartCoroutine("Iceberg_Gen");
                        time_RESPAWN = 0.0f;
                    }
                    time_PHASE += Time.deltaTime;
                }
                else
                {
                    PHASE = 14;
                }
                break;

            case 14:    //번개 이벤트
                if (time_PHASE <= Timer_PHASE)
                {
                    if (time_RESPAWN < Timer_RESPAWN)
                    {
                        time_RESPAWN += Time.deltaTime;
                    }
                    else
                    {
                        StartCoroutine("Thunder_Gen");
                        time_RESPAWN = 0.0f;
                    }
                    time_PHASE += Time.deltaTime;
                }
                else
                {
                    PHASE = 15;
                }
                break;

            case 15:    //거북이 NPC 이벤트
                /*if (time_PHASE <= Timer_PHASE && time_PHASE < 11.0f)
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
                if (time_PHASE < 20.0f && time_PHASE > 11.0f)
                {
                    if (LStar_Count == 0)
                    {
                        StartCoroutine("LStar_Gen");
                        LStar_Count++;
                    }
                    time_PHASE += Time.deltaTime;
                }
                else
                {*/
                PHASE = 16;
                
                break;

            case 16:    //별의 승천 이벤트
                PHASE = 17;
                break;

            case 17:    //연인 이벤트 1(만남)
                PHASE = 18;
                break;

            case 18:    //연인 이벤트 2(프로포즈)
                PHASE = 19;
                break;  

            //******************************************** 노년기 **************************************************//
            //******************************************************************************************************//
            case 19:    //아내의 아픔
                PHASE = 20;
                break;

            case 20:    //절규 이벤트
                PHASE = 21;
                break;

            case 21:    //엔딩
                PHASE = 22;
                break;

            default:    //아무 의미 없는 Default값 
                break;
        }
    }

    //************************************************************************************* FUNCTIONS *****************************************************************************************//
    
    //Getter, Setter
    public int GetPhase()
    {
        return this.PHASE;
    }

    public void SetPhase(int num)
    {
        this.PHASE = num;
    }

    public int GetDialogIndex()
    {
        return this.dialogIndex;
    }

    public void SetDialogIndex(int num)
    {
        this.dialogIndex = num;
    }

    public void ReadJson()
    {
        string fileName = "DialogTable";
        string path = Application.dataPath + "/UsedAssets/" + fileName + ".Json";

        FileInfo fi = new FileInfo(path);
        if (fi.Exists == false)
        {
            Debug.LogError($"Failed to read {fi.Name}");
        }

        string jsonstr = File.ReadAllText(fi.FullName);

        dialogs = JsonUtility.FromJson<Dialogues>(jsonstr);
    }

    public Dialogue chooseDialog()
    {
        Dialogue result = new Dialogue();

        foreach (Dialogue dialog in dialogs.Items)
        {
            if (dialog.phase == PHASE)
            {
                if (dialog.index == dialogIndex)
                {
                    result = dialog;
                }
            }
        }
        dialogIndex++;
        if(result == null)
        {
            Debug.LogError("The Dialogue Object has null value");
        }
        return result;
    }

    // ************************************************************************************ COROUTINES *****************************************************************************************//
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
        int i;
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
        int i;
        for (i = 0; i < 10; i++)
        {
            StarTransform.position = new Vector3(playerTransform.position.x + Random.Range(-1 * min, min), StarTransform.position.y, playerTransform.position.z + Random.Range(min, max));
            GameObject instantStar = Instantiate(Star, StarTransform.position, StarTransform.rotation);
        }
        yield return null;
    }

    IEnumerator LStar_Gen()
    {
        //플레이어 Transform 불러옴
        Transform playerTransform = Player.GetComponent<Transform>();

        //범위 설정 (min, Max값)
        float min = 10.0f;
        float max = 15.0f;

        LStarTransform.position = new Vector3(playerTransform.position.x + Random.Range(-1 * min, min), LStarTransform.position.y, playerTransform.position.z + Random.Range(min, max));
        GameObject instantLStar = Instantiate(LStar, LStarTransform.position, LStarTransform.rotation);

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
        Transform playerTransform = Player.GetComponent<Transform>();

        //범위 설정 (min, Max값)
        float min = 100.0f;
        float max = 150.0f;

        //하트 오브젝트 10개 생성
        int i;
        for (i = 0; i < 2; i++)
        {
            WhirlpoolTransform.position = new Vector3(playerTransform.position.x + Random.Range(-1 * min, min), WhirlpoolTransform.position.y, playerTransform.position.z + Random.Range(min, max));
            GameObject instantWhirlpool = Instantiate(Whirlpool, WhirlpoolTransform.position, WhirlpoolTransform.rotation);
        }
        yield return null;
    }

    IEnumerator Iceberg_Gen()
    {
        Transform playerTransform = Player.GetComponent<Transform>();

        //범위 설정 (min, Max값)
        float min = 100.0f;
        float max = 150.0f;

        //하트 오브젝트 10개 생성
        int i;
        for (i = 0; i < 2; i++)
        {
            IcebergTransform.position = new Vector3(playerTransform.position.x + Random.Range(-1 * min, min), IcebergTransform.position.y, playerTransform.position.z + Random.Range(min, max));
            GameObject instantIceberg = Instantiate(Iceberg, IcebergTransform.position, IcebergTransform.rotation);
        }
        yield return null;
    }

    IEnumerator Iceberg_Wall_Gen()
    {
        /*Transform playerTransform = Player.GetComponent<Transform>();

        //범위 설정 (min, Max값)
        float min = 100.0f;
        float max = 150.0f;


        HeartTransform.position = new Vector3(playerTransform.position.x + Random.Range(-1 * min, min), HeartTransform.position.y, playerTransform.position.z + Random.Range(min, max));
        GameObject instantHeart = Instantiate(Heart, HeartTransform.position, HeartTransform.rotation);
*/
        yield return null;
    }

    IEnumerator Thunder_Gen()
    {
        Transform playerTransform = Player.GetComponent<Transform>();

        //범위 설정 (min, Max값)
        float min = 100.0f;
        float max = 150.0f;

        int i;
        for (i = 0; i < 2; i++)
        {
            ThunderTransform.position = new Vector3(playerTransform.position.x + Random.Range(-1 * min, min), ThunderTransform.position.y, playerTransform.position.z + Random.Range(min, max));
            GameObject instantHeart = Instantiate(Thunder, ThunderTransform.position, ThunderTransform.rotation);
        }
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
    IEnumerator DlTriggerObj_Gen(Dialogue dialog)
    {
        GameObject instantDlTrigObj = Instantiate(DialogueTriggerObject, DlTriggerObjTransform.position, DlTriggerObjTransform.rotation);

        DialogueTrigger dlTrig = instantDlTrigObj.GetComponent<DialogueTrigger>();

        dlTrig.info.index = dialog.index;
        dlTrig.info.phase = dialog.phase;
        dlTrig.info.name = dialog.name;
        dlTrig.info.name2 = dialog.name2;
        foreach (string sentence in dialog.sentences)
        {
            Debug.Log(sentence);
            dlTrig.info.sentences.Add(sentence);
        }
        dlTrig.info.nums = dialog.nums;

        yield return null;
    }
}

