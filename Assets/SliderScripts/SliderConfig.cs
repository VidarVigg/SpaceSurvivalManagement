using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[System.Serializable]
public class SliderConfig 
{
    public SliderManager.EventStruct[] structArray = null;
    public Vector2 minMaxRandomEvent = Vector2.zero;
    public Slider integritySlider;
    public Slider automationSlider;
    public float integritySliderDecreaseAmount;
    public bool automated;
    public float integrityDecreaseDuration;
    public int exchangeAmount;
    public TextMeshProUGUI informationText;
    public TextMeshProUGUI automatedText;
    public float integrityLoseAmt;
}
