using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SliderData
{
    public SliderManager.EventStruct[] structArray = null;
    public Vector2 minMaxRandomEvent = Vector2.zero;
    public Slider integritySlider;
    public Slider automationSlider;
    public float integritySliderDecreaseAmount;
    public float integrityDecreaseDuration;
    public bool automated;
    public int exchangeAmount;
    public Text text;
    public Text automatedText;
}

