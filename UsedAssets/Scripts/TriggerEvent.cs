using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager GManager = FindObjectOfType<GameManager>();
            GManager.PHASE = GManager.PHASE + 1;
            Destroy(gameObject);
        }
    }
}
