using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectPowerManager : MonoBehaviour
{
    [SerializeField] CollectPowerData collectPowerData = new CollectPowerData();
    [SerializeField] CollectPowerConfig collectPowerConfig = new CollectPowerConfig();
    [SerializeField] CollectPowerController collectPowerController = new CollectPowerController();
    private Coroutine rightOrWrongMessageRoutine;


    private void Start()
    {
        collectPowerConfig.combinationText.text = collectPowerController.GenerateRandomStringCombination(collectPowerData, collectPowerConfig, collectPowerData.combination);
    }

    public void CompareTexts(InputField inputField)
    {
        if (inputField.text == collectPowerConfig.combinationText.text)
        {
            rightOrWrongMessageRoutine = StartCoroutine(DisplayRightOrWrongMessageRoutine(collectPowerData.right));
        }
        else
        {
            rightOrWrongMessageRoutine = StartCoroutine(DisplayRightOrWrongMessageRoutine(collectPowerData.wrong));
        }
    }

    private IEnumerator DisplayRightOrWrongMessageRoutine(string message)
    {
        collectPowerConfig.rightOrWrong.text = message;
        yield return new WaitForSeconds(2);
        collectPowerConfig.rightOrWrong.text = null;
        rightOrWrongMessageRoutine = null;
        yield break;
    }
}
