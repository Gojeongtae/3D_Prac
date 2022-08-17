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

    private GameObject heart;

    public int heartcount;
    public Animator anim;

    void Start()
    {
        behavior = GetComponent<Behavior>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        anim = GameObject.Find("Player").GetComponent<Animator>();
    }

    void Update()
    {
        // 마우스 입력을 받았 을 때
        if (Input.GetMouseButtonUp(0))
        {
            // 마우스로 찍은 위치의 좌표 값을 가져온다
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Water")))
            {
                if(hit.transform.tag == "River")
                {
                    targetPos = new Vector3(hit.point.x, 2, hit.point.z);
                    anim.SetTrigger("isRow");
                }
            }
        }

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

        if (isTime)
        {
            //상호작용을 보여주는 대기바
            time += Time.deltaTime;
            RealTime = time * 0.5f;
            UIBar.instance.SetValue(RealTime);

            if (RealTime >= 3)
            {
                time = 0f;
                isTime = false;
                Destroy(heart);
            }
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