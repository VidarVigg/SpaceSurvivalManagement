using System.Collections;
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

    public void CompareTexts(InputField inputField)
    {
        //IncreaseNumberOfIterations();
        if (inputField.text.Length != collectPowerData.combination.Length)
        {
            collectPowerConfig.rightOrWrong.color = new Color(255, 0, 0);
            rightOrWrongMessageRoutine = StartCoroutine(RightOrWrongMessageRoutine(collectPowerData.wrong, inputField));
            return;
        }
        if (inputField.text != collectPowerData.combination)
        {
            collectPowerConfig.rightOrWrong.color = new Color(255, 0, 0);
            rightOrWrongMessageRoutine = StartCoroutine(RightOrWrongMessageRoutine(collectPowerData.wrong, inputField));
            return;
        }
        collectPowerConfig.rightOrWrong.color = new Color(0, 255, 0);
        rightOrWrongMessageRoutine = StartCoroutine(RightOrWrongMessageRoutine(collectPowerData.right, inputField));
        sliderManager.IncreasResourceDirectly(1, 10f);


    }

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

    public void IncreaseNumberOfIterations()
    {
        if (collectPowerConfig.numberOfIterations < 4)
        {
            collectPowerConfig.numberOfIterations += 1;
        }
        else
        {
            DeactivateCollectPowerGame();
            Debug.Log("Close Panel");
        }
    }
    public void ActivateCollectPowerGame()
    {
        collectPowerConfig.collectPowerCanvas.enabled = true;
        collectPowerController.GenerateAndApply(ref collectPowerData, collectPowerConfig, collectPowerConfig.combinationText);
    }
    public void DeactivateCollectPowerGame()
    {
        collectPowerConfig.collectPowerCanvas.enabled = false;
        collectPowerConfig.numberOfIterations = 0;
    }
    public void GenerateNewCode()
    {
        collectPowerController.GenerateAndApply(ref collectPowerData, collectPowerConfig, collectPowerConfig.combinationText);

    }

}
