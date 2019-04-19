using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectO2Manager : MonoBehaviour
{
    public CollectO2Config collectO2Config = new CollectO2Config();
    public CollectO2Data collectO2Data = new CollectO2Data();
    public CollectO2Controller collectO2Controller = new CollectO2Controller();
    public SliderController sliderController = new SliderController();
    Coroutine o2Routine;

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
    }

    private IEnumerator DecreaseO2Value()
    {
        for (; ; )
        {
            sliderController.DecreaseValue(ref collectO2Config.o2Slider, 10f * Time.deltaTime);
            yield return null;
            if (collectO2Config.o2Slider.value < 1)
            {
                o2Routine = null;
                yield break;
            }
        }
    }


}

