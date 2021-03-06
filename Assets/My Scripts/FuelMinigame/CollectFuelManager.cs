﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Main class that controlls the Fuel collecting minigame
public class CollectFuelManager : MonoBehaviour
{
    public CollectFuelConfig collectFuelConfig = new CollectFuelConfig();
    public CollectFuelController collectFuelController = new CollectFuelController();
    public CollectFuelData collectFuelData = new CollectFuelData();
    public SliderManager sliderManager = new SliderManager();
    public bool fuelMinigameActivated;
    private Coroutine spawnButtons;
    private Coroutine spawnGoodButton;
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
    // Activation of Minigame
    public void ActivateCollectFuelMiniGame()
    {
        AudioManager.instance.PlayOneShot(AudioManager.EventType.ButtonSound);
        if (fuelMinigameActivated == false)
        {
            StartFuelMinigame();
        }
        else
        {
            DeactivateFuelMinigame();
        }
    }
    //buttons spawn
    private IEnumerator SpawnButtonsRoutine()
    {
        for (int i = 0; i < collectFuelData.buttons.Length; i++)
        {
            GameObject buttonClone = collectFuelData.buttons[i] = Instantiate(collectFuelData.buttonPrefab, collectFuelData.gridPanel);
            collectFuelController.ActivateBadButton(buttonClone, this);
            yield return new WaitForSeconds(0.005f);
        }
        spawnGoodButton = StartCoroutine(EventifyRandomButton());
        yield break;
    }

    // Random button gets a click event, and a highlight color.
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
        DeactivateFuelMinigame();
        spawnButtons = null;
        yield return null;
        yield break;
    }

    public void StartFuelMinigame()
    {

        fuelMinigameActivated = true;
        ItweenManager.instance.ItweenMoveTo(0);
        if (spawnButtons == null)
        {
            spawnButtons = StartCoroutine(SpawnButtonsRoutine());

        }
    }
    public void DeactivateFuelMinigame()
    {
        if (spawnButtons != null)
        {
            StopCoroutine(spawnButtons);

        }

        if (spawnGoodButton != null)
        {
            StopCoroutine(spawnGoodButton);
        }
        
        spawnButtons = null;
        spawnGoodButton = null;
        fuelMinigameActivated = false;
        ItweenManager.instance.ItweenMoveBack(0);
        NullAllButtons();
    }

    // Reset all buttons
    public void NullAllButtons()
    {
        for (int i = 0; i < collectFuelData.buttons.Length; i++)
        {
            Destroy(collectFuelData.buttons[i]);
            collectFuelData.buttons[i] = null;
        }
    }
    // Resets Clicked Button
    public void NullClickedButton(GameObject button)
    {
        collectFuelController.ActivateBadButton(button, this);
        collectFuelController.DeactivateButton(button, collectFuelData.defaultColor);
    }


}
