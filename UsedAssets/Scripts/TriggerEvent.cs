using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            int num = gameManager.GetPhase();
            gameManager.SetPhase(++num);
            Destroy(gameObject);
        }
    }
}
