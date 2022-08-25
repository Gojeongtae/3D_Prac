using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera thisCamera;
    public Transform follow;

    public float Rotspeed = 15.0f;
    public float Transspeed = 5.0f;
    bool UIisActive = false;

    private int result;

    public GameObject DelayUI;

    private int count; // 몇번 가능하게 할지

    float All;

    // Start is called before the first frame update
    void Start()
    {
        count = 1;
        Rotspeed = 15.0f;
        Transspeed = 5.0f;
        thisCamera = GetComponent<Camera>();
        DelayUI = GameObject.FindGameObjectWithTag("UI");
    }

    // Update is called once per frame
    void Update()
    {
        if(count >= 1 && count <=3)
        {

            float scroll = Input.GetAxis("Mouse ScrollWheel") * Rotspeed;
            All += scroll;
            Debug.Log(All);
            if(All <=0 && All>= -3)
            {
                if (scroll != 0)
                {
                    Quaternion q = follow.rotation;
                    q.eulerAngles = new Vector3(q.eulerAngles.x + scroll * Rotspeed, q.eulerAngles.y, q.eulerAngles.z);
                    follow.transform.position = new Vector3(follow.transform.position.x, follow.transform.position.y + scroll * Transspeed, follow.transform.position.z);
                    follow.rotation = q;
                }
                else if (result != 3)
                {
                    DelayUI.SetActive(false);
                }
                else if (result == 3)
                {
                    DelayUI.SetActive(true);
                }
                result = (int)Mathf.RoundToInt(follow.rotation.x * 10f);
            }
        }
        if(All > 0)
        {
            All = 0;
        }
        if(All < -3)
        {
            All = -3;
        }
    }
}
