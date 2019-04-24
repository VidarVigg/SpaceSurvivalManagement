using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class CollectPowerConfig 
{
    public Canvas collectPowerCanvas;
    public int numberOfIterations;
    public int charCombinationLength;
    public TextMeshProUGUI combinationText;
    public Text rightOrWrong;
}
