using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    private Behavior behavior; // 캐릭터의 행동 스크립트
    private Camera mainCamera; // 메인 카메라
    private Vector3 targetPos; // 캐릭터의 이동 타겟 위치
    private float time;
    private bool isTime;
    public float RealTime;
    private float FirstTime;

    private GameObject heart;

    public int heartcount;
    public Animator anim;
    public int heartInt; //체력
    private int heartMax; //최대체력

    void Start()
    {
        //초기화
        heartInt = 5;
        heartMax = 20;

        behavior = GetComponent<Behavior>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        anim = GameObject.Find("Player").GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Die();
        Fishing();

        // 캐릭터가 움직이고 있다면
        if (behavior.Run(targetPos))
        {
            // 회전도 같이 시켜준다
            behavior.Turn(targetPos);
        }
        else
        {
            // 캐릭터 애니메이션(정지 상태)
            //behavior.SetAnim(PlayerAnim.ANIM_IDLE);
        }
      
    }

    private void Move()
    { 
        // 마우스 입력을 받았 을 때
        if (Input.GetMouseButtonUp(0))
        {
            if (GameObject.FindGameObjectWithTag("DialogUI") == null)
            {
                // 마우스로 찍은 위치의 좌표 값을 가져온다
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
        }
    }

    void Fishing()
    {
        if (isTime)
        {
            //상호작용을 보여주는 대기바
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
            // 피가 0이하면 죽음. 한번 죽었을시 계속 죽지 않도록 isDead의 조건 추가
     
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
            isTime = true;
            heart = other.gameObject;
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
        if (other.tag == "Gacier")
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
            isTime = false;
        }
    }
}