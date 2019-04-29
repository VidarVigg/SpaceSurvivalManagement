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
    public void IncreaseO2ValueOnButtonPress()
    {

        collectO2Controller.IncreaseO2Value(ref collectO2Config.o2Slider, collectO2Config);
        if (collectO2Config.o2Slider.value >= 40 && goal1 == false)
        {
            sliderManager.IncreasResourceDirectly(2, 10);

            goal1 = true;
        }
        if (collectO2Config.o2Slider.value >= 60 && goal2 == false)
        {
            sliderManager.IncreasResourceDirectly(2, 10);

            goal2 = true;
        }
        if (collectO2Config.o2Slider.value >= 80 && goal3 == false)
        {
            sliderManager.IncreasResourceDirectly(2, 10);

            goal3 = true;
        }
        if (collectO2Config.o2Slider.value >= 99 && goal4 == false)
        {
            sliderManager.IncreasResourceDirectly(2, 10);

            goal4 = true;
        }
    }

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
        //collectO2Config.o2MinigameCanvas.enabled = false;
    }


}

