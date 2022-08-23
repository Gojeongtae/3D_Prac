using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{
    public static UIBar instance { get; private set; }

    public Image mask; // 낚시 대기시간 바
    float originalSize = 0;
    public int count = 0;
    public Text text;
    private int H; // 체력 값 가져온 것
    private float H2; // 체력 값 0~1사이 값으로 가져온 것

    public Image Healthmask; // 체력바

    int prev = 0;
    public int curr = 0;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //체력바
        originalSize = 100f;
        prev = UIBar.instance.count;
        curr = UIBar.instance.count;


    }
    private void Update()
    {
        text.text = "아이템 수 : " + count.ToString();

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