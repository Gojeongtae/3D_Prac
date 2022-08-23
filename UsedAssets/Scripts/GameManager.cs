using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager: MonoBehaviour
{
    //������ �� ����� ���� Ÿ�̸� �� ��ġ
    public int PHASE = 0;
    private float Timer_PHASE;
    private float Timer_RESPAWN;
    private float time_PHASE;
    private float time_RESPAWN;

    //�Ĺ� �̺�Ʈ �� �����ؾ� �� GameObject
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
    public Transform LStarTransforml;       // ū �� ������Ʈ�� Transform
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

    public FadeInOut fadeInOut;

    //�÷��̾� ĳ����
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

        //���� ���� �� �غ�
        //�̺�Ʈ Ʈ���� ������Ʈ Transform �ʱ�ȭ
        EvTriggerObjTransform.position = new Vector3(0f, 0f, -35.0f);
        EvTriggerObjTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        StartCoroutine("EvTriggerObj_Gen");


        //fadeInOut = GameObject.Find("FadeInOut").GetComponent<FadeInOut>();
    }

    // Update is called once per frame
    void Update()
    {
        //���� �̺�Ʈ ���� ����
        switch (PHASE)
        {
            //********���� ���� ��********//
            case 0:
                //���̵� ��
                fadeInOut.StartCoroutine(fadeInOut.FadeInStart());
                Debug.Log("a");
                break;

            //********���Ʊ�*********//
            //Case 1 = �̵� Ʃ�丮��
            //Case 2 = ��Ʈ �Ĺ�
            //Case 3 = ���� Ʃ�丮��
            //Case 4 = ���� �� �� ����

            case 1:
                // fadeInOut = GameObject.Find("FadeInOut").GetComponent<FadeInOut>();
                // fadeInOut.StartCoroutine(fadeInOut.FadeInStart());
                // Debug.Log("a");
                //�̵� Ʃ�丮��
                //�θ���� ��ȭ ��� ���� �۾�
                fadeInOut.StartCoroutine(fadeInOut.FadeInStart());
                if (FindObjectOfType<DialogueTrigger>() == null)
                {
                    DlTriggerObjTransform.position = new Vector3(0f, 0.0f, -10f);
                    DlTriggerObjTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                    StartCoroutine("DlTriggerObj_Gen");
                }
                
                //��Ʈ �Ĺ� �̺�Ʈ���� ��ȯ �غ�
                if(FindObjectOfType<TriggerEvent>() == null)
                {
                    EvTriggerObjTransform.position = new Vector3(0f, 0f, 10.0f);
                    EvTriggerObjTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                    StartCoroutine("EvTriggerObj_Gen");
                }
                break;

            case 2: 
                // ��Ʈ �Ĺ�
                //������ Ÿ�̸��� ���̵��� �̺�Ʈ ����
                //�̺�Ʈ ����Ǵ� ���� ���� �ֱ⶧���� ��Ʈ �����
                //�� ���� Ÿ�̸Ӹ� Ȱ��, ������ Ÿ�̸��� 40�ʰ� �Ǿ��� �� ���� ������
                //������ Ÿ�̸Ӵ� Ư�� �ð�(���⼭�� 3��)���� ���ο� ������Ʈ�� ����
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

            case 3:     //���� Ʃ�丮��
                break;
            case 4:     //���� �� �� ���� �̺�Ʈ
                break;

            //********û�ҳ��*********//
            //Case 5 = �� �Ĺ�
            //Case 6 = ū �� �Ĺ�
            //Case 7 = ����
            case 5:     //�� �Ĺ� �̺�Ʈ
                //��Ʈ �Ĺ� �̺�Ʈ�� ����, �ĹֵǴ� 
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
            case 6:     //ū �� �Ĺ� �̺�Ʈ
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
            case 7:     //���� �̺�Ʈ
                break;

            //********�����*********//
            //Case 8 = �� �Ĺ�
            //Case 9 = ���ű� NPC �̺�Ʈ
            //Case 10 = ������ �̺�Ʈ
            //Case 11 = �ҿ뵹�� �̺�Ʈ
            //Case 12 = ���� �̺�Ʈ
            //Case 13 = ���� �̺�Ʈ
            //Case 14 = �ź��� NPC �̺�Ʈ
            //Case 15 = ���� ��õ
            //Case 16 = ���� �̺�Ʈ 1
            //Case 17 = ���� �̺�Ʈ 2

            case 8: //�� �Ĺ� �̺�Ʈ
                break;

            //********����********//
            //Case 18 = 


            default:    //�ƹ� �ǹ� ���� Default�� 
                break;
        }
    }

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
        //�÷��̾� Transform �ҷ���
        Transform playerTransform = Player.GetComponent<Transform>();

        //���� ���� (min, Max��)
        float min = 50.0f;
        float max = 100.0f;

        //�� ������Ʈ 10�� ����
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

    //�� �� �̺�Ʈ �� ȣ���Ͽ��� �� Coroutine ���
    //�ϱ� ��� ��, ū ��, ��, �� ��� ��ȭ Ʈ����,
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

