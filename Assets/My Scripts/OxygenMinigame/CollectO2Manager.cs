using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectO2Manager : MonoBehaviour
{
    public CollectO2Config collectO2Config = new CollectO2Config();
    public CollectO2Data collectO2Data = new CollectO2Data();
    public CollectO2Controller collectO2Controller = new CollectO2Controller();
    public SliderManager sliderManager = new SliderManager();
    Coroutine o2Routine;
    bool minigameActivated = false;
    bool goal1 = false;
    bool goal2 = false;
    bool goal3 = false;
    bool goal4 = false;

    private void Start()
    {
       
    }

    // Increases O2 Value when the slider reaches certain levels.
    public void IncreaseO2ValueOnButtonPress()
    {
        AudioManager.instance.PlayOneShot(AudioManager.EventType.ButtonSound);
        collectO2Controller.IncreaseO2Value(ref collectO2Config.o2Slider, collectO2Config);
        if (sliderManager.sliderData.structArray[2].slider.value == sliderManager.sliderData.structArray[2].slider.maxValue)
        {
            return;
        }

        if (collectO2Config.o2Slider.value >= 30 && goal1 == false)
        {
            sliderManager.IncreasResourceDirectly(2, 10);
            goal1 = true;

            Debug.Log("1");
        }
        if (collectO2Config.o2Slider.value >= 50 && goal2 == false)
        {
            sliderManager.IncreasResourceDirectly(2, 10);
            goal2 = true;

            Debug.Log("2");
        }
        if (collectO2Config.o2Slider.value >= 70 && goal3 == false)
        {
            sliderManager.IncreasResourceDirectly(2, 10);
            goal3 = true;
            Debug.Log("3");
        }
        if (collectO2Config.o2Slider.value >= 90 && goal4 == false)
        {
            sliderManager.IncreasResourceDirectly(2, 10);
            goal4 = true;
            Debug.Log("4");
        }
    }
    // Constantly decreasing o2 value. Purpose of the minigame is to counteract this.
    private IEnumerator DecreaseO2Value()
    {
        for (; ; )
        {
            sliderManager.sliderController.ChangeResourceValues(SliderController.InceaseOrDecrease.Decrease, ref collectO2Config.o2Slider, 10f * Time.deltaTime);
            yield return null;
            if (collectO2Config.o2Slider.value >= collectO2Config.o2Slider.maxValue)
            {
                o2Routine = null;
                DeactivateO2Minigame();
                yield break;
            }
        }
    }

    public void ActivateO2Minigame()
    {
        AudioManager.instance.PlayOneShot(AudioManager.EventType.ButtonSound);
        if (minigameActivated == true)
        {
          DeactivateO2Minigame();
        }
        else
        {
         StartO2Minigame();
        }

    }
    public void StartO2Minigame()
    {
        if (o2Routine == null)
        {
            o2Routine = StartCoroutine(DecreaseO2Value());
        }
        collectO2Config.o2Slider.value = collectO2Config.o2Slider.minValue;
        ItweenManager.instance.ItweenMoveTo(1);
        minigameActivated = true;
    }

    // Resets Minigame
    public void DeactivateO2Minigame()
    {
        StopCoroutine(DecreaseO2Value());
        collectO2Config.o2Slider.value = collectO2Config.o2Slider.minValue;
        ItweenManager.instance.ItweenMoveBack(1);
        goal1 = false;
        goal2 = false;
        goal3 = false;
        goal4 = false;
        minigameActivated = false;
    }


}

