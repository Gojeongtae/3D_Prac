using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public int phase;               //� �̺�Ʈ����
    public int index;               //�� �̺�Ʈ���� �� ��°�� ��ȭ����
    public string name;             //�̸�
    public string name2;            //�̸�2
    public List<string> sentences;  //��ȭ ����
    public List<int> nums;          //��ȭ�ϴ� ���
}
