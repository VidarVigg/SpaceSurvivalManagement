using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectPowerController
{
    public void GenerateAndApply(ref CollectPowerData powerData, CollectPowerConfig powerConfig, TextMeshProUGUI displayText)
    {
        GenerateRandomStringCombination(ref powerData, powerConfig);
        ApplyCharCombo(displayText, ref powerData);
    }

    public void GenerateRandomStringCombination(ref CollectPowerData powerData, CollectPowerConfig powerConfig)
    {

        string result = null;

        for (int i = 0; i < powerConfig.charCombinationLength; i++)
        {
            result += powerData.availableCharacters[Random.Range(0, powerData.availableCharacters.Length)];
        }

        powerData.combination = result;

    }

    public void ApplyCharCombo(TextMeshProUGUI displayText, ref CollectPowerData powerData)
    {

        displayText.text = powerData.combination;
    }



}
