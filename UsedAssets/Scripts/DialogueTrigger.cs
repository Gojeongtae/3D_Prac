using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    bool istri = false;
    public Dialogue info;
    public void OnTriggerEnter(Collider other)
    {
        if (istri == false)
        {
            Time.timeScale = 0;
            Trigger();
        }

    }
    public void Trigger()
    {
        istri = true;
        var system = FindObjectOfType<DialogueSystem>();
        system.Begin(info);
    }
    public void OffTrigger()
    {
        istri = false;
    }
   
}
