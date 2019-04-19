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
    bool activated1 = false;
    bool activated2 = false;
    bool activated3 = false;
    bool activated4 = false;

    private void Start()
    {
       
    }
    public void IncreaseO2ValueOnButtonPress()
    {
        if (o2Routine == null)
        {
            o2Routine = StartCoroutine(DecreaseO2Value());
        }
        collectO2Controller.IncreaseO2Value(ref collectO2Config.o2Slider, collectO2Config);
        if (collectO2Config.o2Slider.value >= 50 && activated1 == false)
        {
            sliderManager.IncreasResourceDirectly(2, 5);
            Debug.Log("Hello1");
            activated1 = true;
        }
        if (collectO2Config.o2Slider.value >= 70 && activated2 == false)
        {
            sliderManager.IncreasResourceDirectly(2, 10);
            Debug.Log("Hello2");
            activated2 = true;
        }
        if (collectO2Config.o2Slider.value >= 80 && activated3 == false)
        {
            sliderManager.IncreasResourceDirectly(2, 25);
            Debug.Log("Hello3");
            activated3 = true;
        }
        if (collectO2Config.o2Slider.value >= 90 && activated4 == false)
        {
            sliderManager.IncreasResourceDirectly(2, 30);
            Debug.Log("Hello4");
            activated4 = true;
        }
    }

    private IEnumerator DecreaseO2Value()
    {
        for (; ; )
        {
            sliderManager.sliderController.DecreaseValue(ref collectO2Config.o2Slider, 10f * Time.deltaTime);
            yield return null;
            if (collectO2Config.o2Slider.value < 1)
            {
                o2Routine = null;
                DeactivateO2Minigame();
                yield break;
            }
        }
    }

    public void ActivateO2Minigame()
    {
        collectO2Config.o2MinigameCanvas.enabled = true;
    }
    public void DeactivateO2Minigame()
    {
        activated1 = false;
        activated2 = false;
        activated3 = false;
        activated4 = false;
        o2Routine = null;
        collectO2Config.o2MinigameCanvas.enabled = false;

    }


}

