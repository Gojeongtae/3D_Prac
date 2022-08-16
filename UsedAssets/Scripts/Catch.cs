using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catch : MonoBehaviour
{
    public AnimationCurve curve;
    bool MouseX; 
    bool MouseY;
    public GameObject Net;


    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

        if (MouseX)
        {
            Cast();
            
        }
        if (MouseY)
        {
            CastUp();
        }


    }
    void GetInput()
    {
        MouseX = Input.GetButton("Fire1");
        MouseY = Input.GetButton("Fire2");
    }

    public void Cast()
    {
        float down = 10 * Time.deltaTime;
        Net.transform.localScale += new Vector3(down, 0, 0);
    }
    public void CastUp()
    {
        float down = 10 * Time.deltaTime;
        Net.transform.localScale += new Vector3(-down, 0, 0);
    }
}
