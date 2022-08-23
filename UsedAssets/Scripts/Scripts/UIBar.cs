using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{
    public static UIBar instance { get; private set; }

    public Image mask; // ���� ���ð� ��
    float originalSize = 0;
    public int count = 0;
    public Text text;
    private int H; // ü�� �� ������ ��
    private float H2; // ü�� �� 0~1���� ������ ������ ��

    public Image Healthmask; // ü�¹�

    int prev = 0;
    public int curr = 0;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //ü�¹�
        originalSize = 100f;
        prev = UIBar.instance.count;
        curr = UIBar.instance.count;


    }
    private void Update()
    {
        text.text = "������ �� : " + count.ToString();

        Control control = GameObject.Find("Boat").GetComponent<Control>();
        H = control.heartInt;
        H2 = H / 20f;
        Healthmask.fillAmount = H2;

        curr = UIBar.instance.count;
    }

    public void SetValue(float value)
    {
        mask.fillAmount += value;
        if (mask.fillAmount >= 1f)
        {
            mask.fillAmount = 0f;
            value = 0f;
            count += 1;
        }
    }

}