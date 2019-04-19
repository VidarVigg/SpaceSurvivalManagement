using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectO2Manager : MonoBehaviour
{
    public CollectO2Config collectO2Config = new CollectO2Config();
    public CollectO2Data collectO2Data = new CollectO2Data();
    public CollectO2Controller collectO2Controller = new CollectO2Controller();
    public SliderController sliderController = new SliderController();

    private void Start()
    {
        StartCoroutine(DecreaseO2Value());
    }
    public void IncreaseO2ValueOnButtonPress()
    {
        collectO2Controller.IncreaseO2Value(ref collectO2Config.o2Slider, collectO2Config);
    }

    private IEnumerator DecreaseO2Value()
    {
        for (float i = collectO2Config.o2Slider.value; i > collectO2Config.o2Slider.minValue; i -= Time.deltaTime)
        {
            sliderController.DecreaseValue(ref collectO2Config.o2Slider,1 * Time.deltaTime);
            if (collectO2Config.o2Slider.value < 1)
            {
                Debug.Log("Finished");
                yield break;
            }
        }
    }


}

