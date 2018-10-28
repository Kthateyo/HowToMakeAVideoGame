using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SliderPercent : MonoBehaviour
{
    Slider slider;
    Text text;

    void Start()
    {
        text = GetComponent<Text>();
        slider = GetComponentInParent<Slider>();
    }

    void Update()
    {
        text.text = System.Math.Round(slider.normalizedValue * 100) + "%";
    }
}