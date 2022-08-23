using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{


    public FadeInOut fadeInOut;

    public Collider other;
    public Transform transform;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine("RespawnFade");
        }
        
    }

    IEnumerator RespawnFade()
    {
        fadeInOut.StartCoroutine(fadeInOut.FadeOutStart());
        yield return new WaitForSeconds(3f);
        other.transform.position = new Vector3(0, 0, 0);

        fadeInOut.StartCoroutine(fadeInOut.FadeInStart());
        yield return null;
    }
}
