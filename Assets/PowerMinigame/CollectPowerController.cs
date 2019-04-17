using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPowerController
{

    public string GenerateRandomStringCombination(CollectPowerData powerData, CollectPowerConfig powerConfig, string combination)
    {
        for (int i = 0; i < powerConfig.charCombinationLength; i++)
        {
            combination += powerData.availableCharacters[Random.Range(0, powerData.availableCharacters.Length)];
        }
        return combination;

    }

}
