﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectPowerManager : MonoBehaviour
{
    [SerializeField] CollectPowerData collectPowerData = new CollectPowerData();
    [SerializeField] CollectPowerConfig collectPowerConfig = new CollectPowerConfig();
    [SerializeField] CollectPowerController collectPowerController = new CollectPowerController();
    [SerializeField] SliderManager sliderManager = new SliderManager();
    private Coroutine rightOrWrongMessageRoutine;
    bool powerMinigameActivated;
    bool submitted;

    // Comparing the texts from the inputfield to the required combination of characters
    public void CompareTexts(InputField inputField)
    {
        if (inputField.text.Length != collectPowerData.combination.Length)
        {
            collectPowerConfig.rightOrWrong.color = new Color(255, 0, 0);
            rightOrWrongMessageRoutine = StartCoroutine(RightOrWrongMessageRoutine(collectPowerData.wrong, inputField));
            AudioManager.instance.PlayOneShot(AudioManager.EventType.NumberFeedbackBad);
            return;
        }
        if (inputField.text != collectPowerData.combination)
        {
            collectPowerConfig.rightOrWrong.color = new Color(255, 0, 0);
            rightOrWrongMessageRoutine = StartCoroutine(RightOrWrongMessageRoutine(collectPowerData.wrong, inputField));
            AudioManager.instance.PlayOneShot(AudioManager.EventType.NumberFeedbackBad);
            return;
        }
        collectPowerConfig.rightOrWrong.color = new Color(0, 255, 0);
        rightOrWrongMessageRoutine = StartCoroutine(RightOrWrongMessageRoutine(collectPowerData.right, inputField));
        sliderManager.IncreasResourceDirectly(1, 10f);
        AudioManager.instance.PlayOneShot(AudioManager.EventType.NumberFeedback);
    }

    // Displays message based on wrong or right input. Also increases the number of input iterations to make the game close after five inputs.
    private IEnumerator RightOrWrongMessageRoutine(string message, InputField inputField)
    {
        collectPowerConfig.numberOfIterations += 1;
        collectPowerConfig.rightOrWrong.text = message;
        yield return new WaitForSeconds(0.5f);
        collectPowerConfig.rightOrWrong.text = null;
        inputField.text = null;
        rightOrWrongMessageRoutine = null;
        if (collectPowerConfig.numberOfIterations < 5)
        {
            GenerateNewCode();
        }
        else
        {
            collectPowerConfig.combinationText.text = null;
            collectPowerConfig.rightOrWrong.text = null;
            inputField.text = null;
            rightOrWrongMessageRoutine = null;
            yield return new WaitForSeconds(0.5f);
            DeactivateCollectPowerGame();
            yield break;
        }
    }

    public void Submit(InputField inputField)
    {
        if (submitted == false)
        {
            submitted = true;
            CompareTexts(inputField);

        }
    }

    public void ActivateCollectPowerGame()
    {
        AudioManager.instance.PlayOneShot(AudioManager.EventType.ButtonSound);
        if (powerMinigameActivated == true)
        {
            DeactivateCollectPowerGame();
        }
        else
        {
            StartCollectPowerMinigame();
        }
    }
    public void StartCollectPowerMinigame()
    {
        submitted = false;
        powerMinigameActivated = true;
        ItweenManager.instance.ItweenMoveTo(2);
        collectPowerController.GenerateAndApply(ref collectPowerData, collectPowerConfig, collectPowerConfig.combinationText);
    }
    public void DeactivateCollectPowerGame()
    {
        powerMinigameActivated = false;
        ItweenManager.instance.ItweenMoveBack(2);
        collectPowerConfig.numberOfIterations = 0;
    }
    public void GenerateNewCode()
    {
        submitted = false;
        collectPowerController.GenerateAndApply(ref collectPowerData, collectPowerConfig, collectPowerConfig.combinationText);

    }

}
