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
        // ���콺 �Է��� �޾� �� ��
        if (Input.GetMouseButtonUp(0))
        {
            // ���콺�� ���� ��ġ�� ��ǥ ���� �����´�
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

        if (isTime)
        {
            //��ȣ�ۿ��� �����ִ� ����
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