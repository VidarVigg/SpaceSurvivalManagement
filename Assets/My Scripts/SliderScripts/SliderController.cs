using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//flyttar siffror från ställen till andra ställen
public class SliderController
{

    public enum InceaseOrDecrease
    {

        Increase,
        Decrease,

    }

    public void ChangeResourceValues(InceaseOrDecrease type, ref Slider slider, float amount = 1)
    {
        switch (type)
        {
            case InceaseOrDecrease.Increase:
                IncreaseValue(ref slider, amount);

                break;
            case InceaseOrDecrease.Decrease:
                DecreaseValue(ref slider, amount);

                break;
            default:
                break;
        }
    }

    void IncreaseValue(ref Slider slider, float amount = 1)
    {

        slider.value += amount;
    }

    void DecreaseValue(ref Slider slider, float amount = 1)
    {
        slider.value -= amount;
    }

    public void ExchangeResources(ref Slider increase, ref Slider decrease, float amt = 1f)
    {

        IncreaseValue(ref increase, amt);
        DecreaseValue(ref decrease, amt + 10);
    }
}
