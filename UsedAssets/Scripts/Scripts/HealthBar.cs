using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image mask;
    float originalSize = 0;

    int prev = 0;
    public int curr = 0;

    void Awake()
    {
        mask.fillAmount = 0.175f;
    }

    void Start()
    {
        originalSize = 100f;
        prev = UIBar.instance.count;
        curr = UIBar.instance.count;
    }
    private void Update()
    {
        //Debug.Log(prev);
        curr = UIBar.instance.count;
        if (prev != curr)
        {
            mask.fillAmount += 0.1f;
            prev++;
        }
    }

}