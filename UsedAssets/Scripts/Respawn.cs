using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public FadeInOut fadeInOut;
    public Collider other;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine("RespawnFade");
        }  
    }

    IEnumerator RespawnFade()
    {
        fadeInOut.StartCoroutine(fadeInOut.FadeOut(2f));

        yield return new WaitForSeconds(2f);
        other.transform.position = new Vector3(0, 0, -10);
        fadeInOut.StartCoroutine(fadeInOut.FadeIn(2f));

        yield return null;
    }
}
