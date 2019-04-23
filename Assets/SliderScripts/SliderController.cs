using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//flyttar siffror från ställen till andra ställen
public class SliderController 
{

    public void IncreaseValue(ref Slider slider, float amount = 1)
    {
        slider.value += amount;
    }

    public void DecreaseValue(ref Slider slider, float amount = 1)
    {
        slider.value -= amount;
    }

    public void ExchangeResources(ref Slider increase, ref Slider decrease, float amt = 1f)
    {
        IncreaseValue(ref increase, amt);
        DecreaseValue(ref decrease, amt + 5);
    }


}
