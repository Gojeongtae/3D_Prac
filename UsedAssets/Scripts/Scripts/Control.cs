using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    private Behavior behavior; // ĳ������ �ൿ ��ũ��Ʈ
    private Camera mainCamera; // ���� ī�޶�
    private Vector3 targetPos; // ĳ������ �̵� Ÿ�� ��ġ
    private float time;
    private bool isTime;
    public float RealTime;
    private float FirstTime;

    private GameObject heart;

    public int heartcount;
    public Animator anim;
    public int heartInt; //ü��
    private int heartMax; //�ִ�ü��

    void Start()
    {
        //�ʱ�ȭ
        heartInt = 5;
        heartMax = 20;
        isTime = false;

        behavior = GetComponent<Behavior>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        anim = GameObject.Find("Player").GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Die();
        Fishing();

        // ĳ���Ͱ� �����̰� �ִٸ�
        if (behavior.Run(targetPos))
        {
            // ȸ���� ���� �����ش�
            behavior.Turn(targetPos);
        }
        else
        {
            // ĳ���� �ִϸ��̼�(���� ����)
            //behavior.SetAnim(PlayerAnim.ANIM_IDLE);
        }
      
    }

    private void Move()
    { 
        // ���콺 �Է��� �޾� �� ��
        if (Input.GetMouseButtonUp(0))
        {
            // ���콺�� ���� ��ġ�� ��ǥ ���� �����´�
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Water")))
            {
                if (hit.transform.tag == "River")
                {
                    targetPos = new Vector3(hit.point.x, 2, hit.point.z);
                    anim.SetTrigger("isRow");
                }
            }
        }
        else
        {

        }
    }

    void Fishing()
    {
        if (isTime)
        {
            //��ȣ�ۿ��� �����ִ� ����
            time += Time.deltaTime;
            RealTime = time * 0.001f;
            UIBar.instance.SetValue(RealTime);

            FirstTime += RealTime;

            if (FirstTime >= 1f)
            {
                time = 0f;
                FirstTime = 0f;
                heartInt += 1;
                isTime = false;
                Destroy(heart);
            }
        }
    }

    void Die()
    {
        if (heartInt <= 0)
        {
            // �ǰ� 0���ϸ� ����. �ѹ� �׾����� ��� ���� �ʵ��� isDead�� ���� �߰�
     
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Island")
        {
            DialogueSystem digSys = FindObjectOfType<DialogueSystem>();
            digSys.dialogueTrigger = other.gameObject.GetComponent<DialogueTrigger>();
        }
        if (other.tag == "Heart")
        {
            heart = other.gameObject;
            isTime = true;
            if (heartInt > heartMax)
                heartInt = heartMax;
        }      
        if(other.tag == "Whi")
        {
            Item item = other.GetComponent<Item>();
            heartInt += item.value;
            if (heartInt > heartMax)
                heartInt = heartMax;
        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Heart")
        {
            heart = other.gameObject;
            heart.GetComponent<Object>().speed = 5.0f;
            heart.GetComponent<Object>().rotSpeed = 100.0f;
            isTime = false;
            
        }
    }
}