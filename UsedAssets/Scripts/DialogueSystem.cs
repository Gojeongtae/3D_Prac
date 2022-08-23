using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public Text txtName;
    public Text txtSentence;
    public GameObject Target;
    public GameObject dialogue;
    public DialogueTrigger dialogueTrigger;


    public GameObject dialogueL;
    public GameObject dialogueR;


    Queue<string> sentences = new Queue<string>();
    Queue<int> nums = new Queue<int>();

    string name;
    string name2;

    private int Ivent; //현재의 말풍선 상태 

    public void Begin(Dialogue info)
    {
        sentences.Clear();
        Target.SetActive(true);
        
        foreach (var sentence in info.sentences)
        {
            sentences.Enqueue(sentence);
        }
        foreach (var num in info.nums)
        {
            nums.Enqueue(num);
        }
        name = info.name;
        name2 = info.name2;

        Next(); 
    }

    public void Next()
    {
        if (sentences.Count == 0)
        {
            End();
            return;
        }
        txtSentence.text = sentences.Dequeue();
        Ivent = nums.Dequeue();

        if(Ivent == 1)
        {
            dialogueL.SetActive(false);
            dialogueR.SetActive(true);
            txtName.text = name;
        }
        else if (Ivent == 2)
        {
            dialogueL.SetActive(true);
            dialogueR.SetActive(false);
            txtName.text = name2;
        }
    }
    public void End()
    {
        Time.timeScale = 1;
        txtSentence.text = string.Empty;
        //dialogueTrigger.OffTrigger();
        Target.SetActive(false);
    }
}
