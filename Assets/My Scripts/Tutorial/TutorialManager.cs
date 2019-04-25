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
    public static int currentPrompt = 0;
    public CollectFuelManager collectFuelManager;
    public CollectO2Manager collectO2Manager;
    public CollectPowerManager collectPowerManager;
    public SliderManager sliderManager;
    public GameObject nextButton;
    

    private void Awake()
    {
        tutorialText.text = prompts[currentPrompt].tutorialMessage;
    }

    public void TutorialProgressFwd()
    { 
        if (currentPrompt == 10)
        {
            return;
        }
            currentPrompt += 1;
            TutorialProgress(currentPrompt);
        
    }
    public void TutorialProgressBack()
    {
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
        if (currentPrompt == 4)
        {
            collectFuelManager.ActivateCollectFuelMiniGame();
        }
        if(currentPrompt == 5)
        {
            collectO2Manager.ActivateO2Minigame();
            ItweenManager.instance.ItweenMoveBack(0);
        }
        if (currentPrompt == 6)
        {
            collectPowerManager.ActivateCollectPowerGame();
            ItweenManager.instance.ItweenMoveBack(1);
        }
        if (currentPrompt == 7)
        {
            ItweenManager.instance.ItweenMoveBack(2);
            sliderManager.sliderData.text.text = "Catastrophe Notifications";

        }
        if (currentPrompt == 8)
        {
            sliderManager.sliderData.text.text = null;

        }
        if (currentPrompt == 9)
        {
            ItweenManager.instance.ItweenMoveBack(3);
        }
        if (currentPrompt == 10)
        {

            ItweenManager.instance.ItweenMoveTo(3);

        }
    }
    public void PunchToHilightPanel(RectTransform panel,int index)
    {
        iTween.PunchScale(prompts[index].panel.gameObject,new Vector3(0.5f, 0.5f, 0.5f), 2f);
    }
    public void StartGame()
    {
        ItweenManager.instance.ItweenMoveBack(3);
        StartCoroutine(StartGameRoutine());
    }
    private IEnumerator StartGameRoutine()
    {
        for (float i = 5; i > 0; i -= Time.deltaTime)
        {
            tutorialText.text = prompts[currentPrompt].tutorialMessage + i.ToString("0.0") + " sec.";
            yield return null;
        }
            SceneManager.LoadScene("SampleScene");
        TutorialController.instance.TutorialActive = false;
        yield break;
    }

}
