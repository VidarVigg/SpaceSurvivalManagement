using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectO2Controller 
{
    public void IncreaseO2Value(ref Slider O2Slider, CollectO2Config o2Config)
    {
        O2Slider.value += o2Config.increaseAmount;
    }
    public IEnumerator DecreaseValue()
    {
         
        yield break;
    }


}
