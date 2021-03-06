﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class SliderData
{
    public SliderManager.EventStruct[] structArray = null;
    public Vector2 minMaxRandomEvent = Vector2.zero;
    public Transform textFeedbackCanvas;
    public Slider integritySlider;
    public Slider automationSlider;
    public float integritySliderDecreaseAmount;
    public float integrityDecreaseDuration;
    public bool automated;
    public int exchangeAmount;
    public TextMeshProUGUI informationText;
    public TextMeshProUGUI automatedText;
    public TextMeshProUGUI integrityText;
}

