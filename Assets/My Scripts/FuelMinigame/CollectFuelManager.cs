using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectFuelManager : MonoBehaviour
{
    public CollectFuelConfig collectFuelConfig = new CollectFuelConfig();
    public CollectFuelController collectFuelController = new CollectFuelController();
    public CollectFuelData collectFuelData = new CollectFuelData();
    public SliderManager test = new SliderManager();
    public enum ResourceType
    {
        Fuel, 
        Power, 
        Oxygen,
    }

    private void Awake()
    {
        collectFuelData.buttonPrefab = collectFuelConfig.buttonPrefab;
        collectFuelData.gridPanel = collectFuelConfig.gridPanel;
        collectFuelData.buttons = collectFuelConfig.buttons;
        collectFuelData.newColor = collectFuelConfig.newColor;
        collectFuelData.defaultColor = collectFuelConfig.defaultColor;
        collectFuelData.collectFuelCanvas = collectFuelConfig.collectFuelCanvas;

    }

    private Coroutine routine;
    public void ActivateCollectFuelMiniGame()
    {
        //collectFuelData.collectFuelCanvas.enabled = true;
            ItweenManager.instance.ItweenMoveTo(0);
        if (routine == null)
        {
            routine = StartCoroutine(SpawnButtonsRoutine());

        }
    }
    private IEnumerator SpawnButtonsRoutine()
    {
        for (int i = 0; i < collectFuelData.buttons.Length; i++)
        {
            GameObject buttonClone = collectFuelData.buttons[i] = Instantiate(collectFuelData.buttonPrefab, collectFuelData.gridPanel);
            collectFuelController.ActivateBadButton(buttonClone, this);
            yield return new WaitForSeconds(0.005f);
        }
        StartCoroutine(EventifyRandomButton());
        yield break;
    }
    private IEnumerator EventifyRandomButton()
    {
        for (int j = 0; j < 5; j++)
        {
            int rand = Random.Range(0, collectFuelData.buttons.Length);
            collectFuelController.ActivateGoodButton(this, collectFuelData.buttons[rand], collectFuelData.newColor);
            yield return new WaitForSeconds(1f);
            collectFuelController.DeactivateButton(collectFuelData.buttons[rand], collectFuelData.defaultColor);
            collectFuelController.ActivateBadButton(collectFuelData.buttons[rand], this);

        }
        yield return new WaitForSeconds(1);
        //collectFuelData.collectFuelCanvas.enabled = false;
        ItweenManager.instance.ItweenMoveBack(0);
        NullAllButtons();
        routine = null;
        yield return null;
        yield break;
    }
    public void NullAllButtons()
    {
        for (int i = 0; i < collectFuelData.buttons.Length; i++)
        {
            Destroy(collectFuelData.buttons[i]);
            collectFuelData.buttons[i] = null;
        }
    }
    public void NullClickedButton(GameObject button)
    {
        collectFuelController.ActivateBadButton(button, this);
        collectFuelController.DeactivateButton(button, collectFuelData.defaultColor);
    }


}
