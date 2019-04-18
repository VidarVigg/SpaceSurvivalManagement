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
        collectPowerController.GenerateAndApply( ref collectPowerData, collectPowerConfig, collectPowerConfig.combinationText);
        //collectPowerController.GenerateRandomStringCombination(ref collectPowerData, collectPowerConfig);
    }

    public void CompareTexts(InputField inputField)
    {
        if (inputField.text.Length != collectPowerData.combination.Length)
        {
            rightOrWrongMessageRoutine = StartCoroutine(DisplayRightOrWrongMessageRoutine(collectPowerData.wrong));
            return;
        }
        if (inputField.text != collectPowerData.combination)
        {
            rightOrWrongMessageRoutine = StartCoroutine(DisplayRightOrWrongMessageRoutine(collectPowerData.wrong));
            return;
        }
            rightOrWrongMessageRoutine = StartCoroutine(DisplayRightOrWrongMessageRoutine(collectPowerData.right));
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
