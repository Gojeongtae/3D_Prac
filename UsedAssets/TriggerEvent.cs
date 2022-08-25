using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Player")
        {
            GameManager GManager = FindObjectOfType<GameManager>();
            GManager.SetPhase(GManager.GetPhase() + 1);
            GManager.SetDialogIndex(0);
            Destroy(gameObject);
        }
    }
}
