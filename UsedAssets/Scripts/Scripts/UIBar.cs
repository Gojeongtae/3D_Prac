using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{
    public static UIBar instance { get; private set; }

    public Image mask;
    float originalSize;
    int count = 0;
    public Text text;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
    }
    private void Update()
    {
        text.text = "아이템 수 : " + count.ToString();
    }

    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
        if(value >= 3)
        {
            value = 0f;
            count += 1;
        }
        
    }
}