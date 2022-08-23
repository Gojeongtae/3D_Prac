using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public int phase;               //어떤 이벤트인지
    public int index;               //그 이벤트에서 몇 번째의 대화인지
    public string name;             //이름
    public string name2;            //이름2
    public List<string> sentences;  //대화 내용
    public List<int> nums;          //대화하는 사람
}
