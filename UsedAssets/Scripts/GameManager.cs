using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class GameManager : MonoBehaviour
{
    // **************************************************************************** Event Variables ************************************************************************************ //
    //When The Farming Event GameObject
    public GameObject Heart;                // ��Ʈ ������Ʈ
    public GameObject Star;                 // �� ������Ʈ
    public GameObject LStar;                // ū �� ������Ʈ
    public GameObject Money;                // �� ������Ʈ
    public GameObject Note;                 // ��ǥ ������Ʈ
    public GameObject Flower;               // ���� �̺�Ʈ �� �� ������Ʈ
    public GameObject Lily;                 // ���� ������Ʈ

    //���ű���� �̺�Ʈ �� �����ؾ� �� GameObject
    public GameObject Enemy;                // ������ ������Ʈ
    public GameObject Whirlpool;            // �ҿ뵹�� ������Ʈ
    public GameObject Iceberg;              // ���� ������Ʈ
    public GameObject Iceberg_Wall;         // ���Ϻ� ������Ʈ
    public GameObject Thunder;              // ���� ������Ʈ

    //�� �� �̺�Ʈ �� �����ؾ� �� GameObject
    public GameObject EventTriggerObject;   // �̺�Ʈ �۵���Ű�� �� ������Ʈ
    public GameObject DialogueTriggerObject;// ��ȭ �۵��ñ�Ű�� �� ������Ʈ
    public GameObject BottleWithDiary;      // �ϱ� �� �� ������Ʈ
    public GameObject Boat_Big;             // ū �� ������Ʈ
    public GameObject Island;               // �� ������Ʈ
    public GameObject Rock;                 // �� ������Ʈ

    //�Ĺ� �̺�Ʈ �� �����Ǵ� GameObject�� Transform Component
    public Transform HeartTransform;        // ��Ʈ ������Ʈ�� Transform
    public Transform StarTransform;         // �� ������Ʈ�� Transform
    public Transform LStarTransform;       // ū �� ������Ʈ�� Transform
    public Transform MoneyTransform;        // �� ������Ʈ�� Transform
    public Transform NoteTransform;         // ��ǥ ������Ʈ�� Transform
    public Transform FlowerTransform;       // �� ������Ʈ�� Transform
    public Transform LilyTransform;         // ���� ������Ʈ�� Transform

    //���ű� �̺�Ʈ �� �����Ǵ� GameObject�� Transform Component
    public Transform EnemyTransform;        // ������ ������Ʈ�� Transform
    public Transform WhirlpoolTransform;    // �ҿ뵹�� ������Ʈ�� Transform
    public Transform IcebergTransform;      // ���� ������Ʈ�� Transform
    public Transform Iceberg_WallTransform; // ���Ϻ� ������Ʈ�� Transform
    public Transform ThunderTransform;      // ���� ������Ʈ�� Transform

    //�� �� �̺�Ʈ �� �����Ǵ� GameObject�� Transform Component
    public Transform EvTriggerObjTransform;
    public Transform DlTriggerObjTransform;
    public Transform BottleWDTransform;
    public Transform Boat_BigTransform;
    public Transform IslandTransform;
    public Transform RockTransform;


    // ************************************************************************************* GamePlay Variables ********************************************************************************** //

    //�÷��̾� ĳ����
    private GameObject Player;
    private GameObject Boat;

    //������ �� ����� ���� Ÿ�̸� �� ��ġ
    private int PHASE = 0;
    private float Timer_PHASE;
    private float Timer_RESPAWN;
    private float time_PHASE;
    private float time_RESPAWN;

    public FadeInOut fadeInOut;

    //����� Dialogues�� ������, �ε��� ���
    private Dialogues dialogs;
    private int dialogIndex = 0;


    //����� ������Ʈ ���� ���� ����
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

        //���� �̺�Ʈ ���� ����
        switch (PHASE)
        {
            //******************************************** ���� ���� �� ********************************************//
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

            //*********************************************** ���Ʊ� ***********************************************//
            //******************************************************************************************************//
            //�̵� Ʃ�丮�� -> ��Ʈ �Ĺ�    -> ���� Ʃ�丮�� -> ���� �� �� ����
            

            case 1:     //Ŭ�� �̵� Ʃ�丮��

                //�θ���� ��ȭ ��� ���� �۾�
                if (FindObjectOfType<DialogueTrigger>() == null)
                {
                    Dialogue dialog = new Dialogue();
                    //��ȭ ���� �߰�
                    DlTriggerObjTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z + 20.0f);
                    DlTriggerObjTransform.rotation = Quaternion.Euler(Vector3.zero);
                    dialog = chooseDialog();

                    if(dialog.sentences != null) 
                    {
                        StartCoroutine("DlTriggerObj_Gen", dialog);
                    }
                }
                
                //��Ʈ �Ĺ� �̺�Ʈ���� ��ȯ �غ�
                if (FindObjectOfType<TriggerEvent>() == null && FindObjectOfType<DialogueTrigger>() == null)
                {
                    EvTriggerObjTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z + 15.0f);
                    EvTriggerObjTransform.rotation = Quaternion.Euler(Vector3.zero);
                    StartCoroutine("EvTriggerObj_Gen");
                }
                break;

            case 2:     // ��Ʈ �Ĺ� �̺�Ʈ

                //������ Ÿ�̸��� ���̵��� �̺�Ʈ ����
                if (time_PHASE <= Timer_PHASE)
                {
                    //�̺�Ʈ ����Ǵ� ���� ���� �ֱ⸶�� ��Ʈ �����
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
                //���� ��������� �̵�
                else
                {
                    PHASE = 3;
                    time_PHASE = 0.0f;
                    time_RESPAWN = 0.0f;
                }
                break;

            case 3:     //���� Ʃ�丮��

                //���� ������ ���� (60��)
                Timer_PHASE = 199999999.0f;
                
                if (time_PHASE <= Timer_PHASE)
                {
                    //���� Ʃ�丮�� ���� �� 10�� �� ��ȭ ����
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
                    //���� Ʃ�丮�� ����
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
                    //���� ��������� �̵�
                    PHASE = 4;

                    //Ÿ�̸� ���� �ʱ�ȭ
                    time_PHASE = 0.0f;
                    time_RESPAWN = 0.0f;
                    Timer_PHASE = 0.0f;
                    Timer_RESPAWN = 0.0f;
                }
                break;

            case 4:     //���� �� �� ���� �̺�Ʈ
                Timer_PHASE = 60.0f;
                Timer_RESPAWN = 10.0f;
                //���� ���� �� ��ȭ
                if (FindObjectOfType<DialogueTrigger>() == null)
                {
                    DlTriggerObjTransform.position = new Vector3(0f, 0.0f, -10f);
                    DlTriggerObjTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                    StartCoroutine("DlTriggerObj_Gen");
                }


                //���� ��� �� ��ȭ
                if (FindObjectOfType<DialogueTrigger>() == null)
                {
                    DlTriggerObjTransform.position = new Vector3(0f, 0.0f, -10f);
                    DlTriggerObjTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                    StartCoroutine("DlTriggerObj_Gen");
                }
                //��Ʈ �Ĺ� �̺�Ʈ���� ��ȯ �غ�
                if (FindObjectOfType<TriggerEvent>() == null)
                {
                    EvTriggerObjTransform.position = new Vector3(0f, 0f, 10.0f);
                    EvTriggerObjTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                    StartCoroutine("EvTriggerObj_Gen");
                }
                break;

            //********************************************** û�ҳ�� **********************************************//
            //******************************************************************************************************//
            //�� ���� Ʃ�丮�� -> �� �Ĺ� �̺�Ʈ -> ū �� �Ĺ� �̺�Ʈ -> ���� �̺�Ʈ
            
            case 5:     //�� ���� (Ŭ��-�巡�� �̵�) Ʃ�丮��
                break;

            case 6:     //�� �Ĺ� �̺�Ʈ
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

            case 7:     //ū �� �Ĺ� �̺�Ʈ

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

            case 8:     //���� �̺�Ʈ
                //�Ƶ�� �θ���� ��ȭ
                if (FindObjectOfType<DialogueTrigger>() == null)
                {
                    DlTriggerObjTransform.position = new Vector3(0f, 0.0f, -10f);
                    DlTriggerObjTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                    StartCoroutine("DlTriggerObj_Gen");
                }

                //��Ʈ �Ĺ� �̺�Ʈ���� ��ȯ �غ�
                if (FindObjectOfType<TriggerEvent>() == null)
                {
                    EvTriggerObjTransform.position = new Vector3(0f, 0f, 10.0f);
                    EvTriggerObjTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                    StartCoroutine("EvTriggerObj_Gen");
                }
                break;

            //******************************************** ����� **************************************************//
            //******************************************************************************************************//
            //�� �Ĺ� �̺�Ʈ  -> ���ű� NPC �̺�Ʈ  -> ������ �̺�Ʈ      -> �ҿ뵹�� �̺�Ʈ     -> ���� �̺�Ʈ   -> 
            //���� �̺�Ʈ     -> �ź��� NPC �̺�Ʈ  -> ���� ��õ �̺�Ʈ   -> ���� �̺�Ʈ 1(����) -> ���� �̺�Ʈ 2(��������)

            case 9:     //�� �Ĺ� �̺�Ʈ
                break;

            case 10:    //���ű� NPC �̺�Ʈ
                break;

            case 11:    //������ �̺�Ʈ
                break;

            case 12:    //�ҿ뵹�� �̺�Ʈ
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

            case 13:    //���� �̺�Ʈ
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

            case 14:    //���� �̺�Ʈ
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

            case 15:    //�ź��� NPC �̺�Ʈ
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

            case 16:    //���� ��õ �̺�Ʈ
                PHASE = 17;
                break;

            case 17:    //���� �̺�Ʈ 1(����)
                PHASE = 18;
                break;

            case 18:    //���� �̺�Ʈ 2(��������)
                PHASE = 19;
                break;  

            //******************************************** ���� **************************************************//
            //******************************************************************************************************//
            case 19:    //�Ƴ��� ����
                PHASE = 20;
                break;

            case 20:    //���� �̺�Ʈ
                PHASE = 21;
                break;

            case 21:    //����
                PHASE = 22;
                break;

            default:    //�ƹ� �ǹ� ���� Default�� 
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
    // �Ĺ� �̺�Ʈ �� ȣ���Ͽ��� �� Coroutine ���
    // ��Ʈ, ��, ū ��, ��, ��ǥ, ���� �̺�Ʈ �� �� ������Ʈ, ���� ������Ʈ 
    IEnumerator Heart_Gen()
    {
        //�÷��̾� Transform �ҷ���
        Transform playerTransform = Player.GetComponent<Transform>();

        //���� ���� (min, Max��)
        float min = 50.0f;
        float max = 100.0f;

        //��Ʈ ������Ʈ 10�� ����
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
        //�÷��̾� Transform �ҷ���
        Transform playerTransform = Player.GetComponent<Transform>();

        //���� ���� (min, Max��)
        float min = 50.0f;
        float max = 100.0f;

        //�� ������Ʈ 10�� ����
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
        //�÷��̾� Transform �ҷ���
        Transform playerTransform = Player.GetComponent<Transform>();

        //���� ���� (min, Max��)
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

    // ���ű���� �̺�Ʈ(�÷�) �� ȣ���Ͽ��� �� Coroutine ���
    // ������, �ҿ뵹��, ����, ���Ϻ�, ����
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

        //���� ���� (min, Max��)
        float min = 100.0f;
        float max = 150.0f;

        //��Ʈ ������Ʈ 10�� ����
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

        //���� ���� (min, Max��)
        float min = 100.0f;
        float max = 150.0f;

        //��Ʈ ������Ʈ 10�� ����
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

        //���� ���� (min, Max��)
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

        //���� ���� (min, Max��)
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

    //�� �� �̺�Ʈ �� ȣ���Ͽ��� �� Coroutine ���
    //�ϱ� ��� ��, ū ��, ��, �� ��� ��ȭ Ʈ����,
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

