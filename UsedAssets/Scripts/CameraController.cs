using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera thisCamera;
    public Transform follow;

    public float speed = 10.0f;
    bool UIisActive = false;

    private int result;

    public GameObject DelayUI;
    // Start is called before the first frame update
    void Start()
    {
        thisCamera = GetComponent<Camera>();
        DelayUI = GameObject.FindGameObjectWithTag("UI");
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * speed;

        result = (int)Mathf.RoundToInt(follow.rotation.x * 10f);

        Debug.Log(result);

        //if(result >= 3 && result <= 5)
        
            if (scroll != 0)
            {
                Quaternion q = follow.rotation;
                q.eulerAngles = new Vector3(q.eulerAngles.x + scroll * speed, q.eulerAngles.y, q.eulerAngles.z);
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
        
    }
}
