using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{

    public TextMeshProUGUI tutorialText;
    public TutorialData[] prompts = new TutorialData[10];
    public int currentPrompt = 0;
    public CollectFuelManager collectFuelManager;
    public CollectO2Manager collectO2Manager;
    public CollectPowerManager collectPowerManager;
    public SliderManager sliderManager;
    public GameObject nextButton;
    bool startButtonActive;
    public bool fuelGame;
    public bool o2Game;
    public bool powerGame;
    public bool informationPanel;

    private void Awake()
    {
        tutorialText.text = prompts[currentPrompt].tutorialMessage;
    }
    public void TutorialProgressFwd()
    {
        AudioManager.instance.PlayOneShot(AudioManager.EventType.ButtonSound);
        if (currentPrompt == 10)
        {
            return;
        }
        currentPrompt += 1;
        TutorialProgress(currentPrompt);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            TutorialProgressFwd();

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TutorialProgressBack();
        }
    }
    public void TutorialProgressBack()
    {
        AudioManager.instance.PlayOneShot(AudioManager.EventType.ButtonSound);
        if (currentPrompt != 0)
        {
            currentPrompt -= 1;
            TutorialProgress(currentPrompt);
        }


    }
    public void TutorialProgress(int progress)
    {
        tutorialText.text = prompts[progress].tutorialMessage;
        if (prompts[currentPrompt].punch != false)
        {
            PunchToHilightPanel(prompts[progress].panel, currentPrompt);
        }

        switch (progress)
        {

            case 1:
                break;
            case 3:
                if (fuelGame == true)
                {
                    collectFuelManager.DeactivateFuelMinigame();
                    fuelGame = false;
                }
                break;
            case 4:
                fuelGame = true;
                collectFuelManager.ActivateCollectFuelMiniGame();
                if (o2Game == true)
                {
                    collectO2Manager.DeactivateO2Minigame();
                    o2Game = false;
                }
                break;
            case 5:
                if (fuelGame == true)
                {
                    collectFuelManager.DeactivateFuelMinigame();
                    fuelGame = false;
                }
                if (powerGame == true)
                {
                    collectPowerManager.DeactivateCollectPowerGame();
                    powerGame = false;
                }
                collectO2Manager.ActivateO2Minigame();
                o2Game = true;
                break;
            case 6:
                if (o2Game == true)
                {
                    collectO2Manager.DeactivateO2Minigame();
                    o2Game = false;
                }
                if(informationPanel == true)
                {
                    sliderManager.sliderData.informationText.text = null;
                    ItweenManager.instance.ItweenMoveBack(3);
                    informationPanel = false;
                }
                collectPowerManager.ActivateCollectPowerGame();
                powerGame = true;
                break;
            case 7:
                if (powerGame == true)
                {
                    collectPowerManager.DeactivateCollectPowerGame();
                    powerGame = false;
                }
                ItweenManager.instance.ItweenMoveTo(3);
                sliderManager.sliderData.informationText.text = "Catastrophe Notifications";
                informationPanel = true;
                break;
            case 8:
                sliderManager.sliderData.informationText.text = null;
                ItweenManager.instance.ItweenMoveBack(3);
                informationPanel = false;
                break;
            case 9:
                if (startButtonActive)
                {
                    ItweenManager.instance.ItweenMoveBack(7);
                }
                break;
            case 10:
                ItweenManager.instance.ItweenMoveTo(7);
                startButtonActive = true;
                break;
        }
    }
    public void PunchToHilightPanel(RectTransform panel, int index)
    {
        iTween.PunchScale(prompts[index].panel.gameObject, new Vector3(0.5f, 0.5f, 0.5f), 1f);
    }
    public void StartGame()
    {
        ItweenManager.instance.ItweenMoveTo(3);
        ItweenManager.instance.ItweenMoveBack(7);
        StartCoroutine(StartGameRoutine());
    }
    private IEnumerator StartGameRoutine()
    {
        for (float i = 5; i > 0; i -= Time.deltaTime)
        {
            sliderManager.sliderData.informationText.text = prompts[currentPrompt].tutorialMessage + i.ToString("0.0") + " sec.";
            yield return null;
        }
        SceneManager.LoadScene("SampleScene");
        TutorialController.instance.TutorialActive = false;
        yield break;
    }

}
