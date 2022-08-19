using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{
    public static UIBar instance { get; private set; }

   

    public Image mask;
    float originalSize = 0;
    public int count = 0;
    public Text text;
    void Awake()
    {
        instance = this;
        mask.fillAmount = 0f;
    }

    void Start()
    {
        originalSize = 100f;
    }
    private void Update()
    {
        text.text = "아이템 수 : " + count.ToString();
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