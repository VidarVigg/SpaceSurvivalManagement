using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{

    public SliderConfig sliderConfig = new SliderConfig();
    public SliderController sliderController = new SliderController();
    public SliderData sliderData = new SliderData();
    public Coroutine automationRout;
    public Coroutine chargeAutomationRout;

    [System.Serializable]
    public struct EventStruct
    {

        public Slider slider;
        public string message;
        public Coroutine routine;
        public Slider counterSlider;


        public EventStruct(Slider slider, string message, Coroutine routine, Slider counterSlider)
        {
            this.slider = slider;
            this.message = message;
            this.routine = routine;
            this.counterSlider = counterSlider;
        }
    }

    private void Awake()
    {
        sliderData.structArray = sliderConfig.structArray;
        sliderData.minMaxRandomEvent = sliderConfig.minMaxRandomEvent;
        sliderData.integritySlider = sliderConfig.integritySlider;
        sliderData.integritySliderDecreaseAmount = sliderConfig.integritySliderDecreaseAmount;
        sliderData.integrityDecreaseDuration = sliderConfig.integrityDecreaseDuration;
        sliderData.exchangeAmount = sliderConfig.exchangeAmount;
        sliderData.text = sliderConfig.text;
        sliderData.automationSlider = sliderConfig.automationSlider;
        sliderData.automated = sliderConfig.automated;
        sliderData.automatedText = sliderConfig.automatedText;
    }
    private void Start()
    {
        StartCoroutine(RandomEventRoutine());
    }
    public void CheckIntegrity()
    {
        if (sliderData.integritySlider.value <= 1 /*sliderConfig.integrityLoseAmt*/)
        {
            //sliderData.text.text = GameController.instance.GameOver();
            GameController.instance.GameOver();
        }
    }

    private IEnumerator RandomEventRoutine()
    {


        while (true)
        {
            int random = Random.Range(0, sliderData.structArray.Length);
            yield return new WaitForSeconds(Random.Range(sliderData.minMaxRandomEvent.x, sliderData.minMaxRandomEvent.y));
            for (float i = 5; i > 0; i -= Time.deltaTime)
            {
                sliderData.text.text = "Incoming " + sliderData.structArray[random].message + " in " + i.ToString("0.0");
                yield return null;
            }
            sliderData.structArray[random].routine = StartCoroutine(EventCoroutine(random));
            sliderData.text.text = sliderData.structArray[random].message;
            yield return new WaitUntil(() => sliderData.structArray[random].routine == null);
            sliderData.text.text = null;

        }



    }
    private IEnumerator EventCoroutine(int index)
    {
        if (sliderData.automated == false)
        {
            for (float i = 0; i < sliderData.integrityDecreaseDuration; i += Time.deltaTime)
            {
                sliderController.DecreaseValue(ref sliderData.integritySlider, sliderData.integritySliderDecreaseAmount * Time.deltaTime);
                yield return null;
            }
            sliderData.structArray[index].routine = null;
            yield break;
        }

    }
    private IEnumerator DecreaseIntegrityDueToLackOfResourcesRoutine()
    {

            for (; ;)
            {
                sliderController.DecreaseValue(ref sliderData.integritySlider, sliderData.integritySliderDecreaseAmount * Time.deltaTime);
                yield return null;
            }
            yield break;
        

    }
    private Coroutine dec;
    public void DecreaseIntegrityDueToLackOfResources(Slider slider)
    { 
        if(slider.value < 10)
        {
            if(dec == null)
            {

            dec = StartCoroutine(DecreaseIntegrityDueToLackOfResourcesRoutine()); //if not already active
            }
        }
        else
        {
            StopDecreaseIntegrityDueToLackOfResources();
        }

    }
    public void StopDecreaseIntegrityDueToLackOfResources()
    {

        for (int i = 0; i < sliderData.structArray.Length; i++)
        {
            if (sliderData.structArray[i].slider.value < 10)
            {
                return;
            }

        }
        
        if(dec != null)
        {
        StopCoroutine(dec);

        dec = null;
        }
    }



    public void StopEvent(int index)
    {
        if (sliderData.structArray[index].routine != null)
        {
            if (sliderData.structArray[index].counterSlider.value >= 20)
            {
                
                StopCoroutine(sliderData.structArray[index].routine);
                sliderData.structArray[index].routine = null;
                sliderController.DecreaseValue(ref sliderData.structArray[index].counterSlider, 20);

            }
            else
            {
                sliderData.text.text = "Insufficient Resource!!1!!111 1 1!";
            }
        }
    }

    public void ExchangeResources(int index)
    {

        if (sliderData.structArray[index].counterSlider.value < sliderData.exchangeAmount || sliderData.structArray[index].slider.value + sliderData.exchangeAmount > sliderData.structArray[index].slider.maxValue)
        {

            if (sliderData.structArray[index].counterSlider.value < sliderData.exchangeAmount)
            {
                sliderController.ExchangeResources(ref sliderData.structArray[index].slider, ref sliderData.structArray[index].counterSlider, sliderData.structArray[index].counterSlider.value);
            }

            else
            {
                sliderController.ExchangeResources(ref sliderData.structArray[index].slider, ref sliderData.structArray[index].counterSlider, sliderData.structArray[index].slider.maxValue - sliderData.structArray[index].slider.value);
            }

        }
        else
        {
            sliderController.ExchangeResources(ref sliderData.structArray[index].slider, ref sliderData.structArray[index].counterSlider, sliderData.exchangeAmount);
        }

    }

    public void IncreasResourceDirectly(int index, float amount)
    {
        sliderController.IncreaseValue(ref sliderData.structArray[index].slider, amount);

    }
    public void DecreaseResourceDirectly(int index, float amount)
    {
        sliderController.DecreaseValue(ref sliderData.structArray[index].slider, amount);
        if (sliderData.structArray[index].slider.value <= 6)
        {
            Debug.Log("ResourceDepleted");
        }
    }
    public void ActivateCounterMeasureAutomation()
    {

        if (automationRout == null)
        {
            automationRout = StartCoroutine(CounterMesureAutomationRoutine());
        }


    }
    private IEnumerator CounterMesureAutomationRoutine()
    {
        sliderData.automated = true;
        sliderData.automatedText.text = "Counter Measures Automated";
        for (float i = sliderData.automationSlider.value; i > 0; i -= Time.deltaTime)
        {
            sliderController.DecreaseValue(ref sliderData.automationSlider, 0.05f);
            if (sliderData.automationSlider.value <= 2)
            {
                sliderData.automated = false;
                Debug.Log("Finished");
                sliderData.automatedText.text = null;
                sliderData.text.text = null;
                automationRout = null;
                if (chargeAutomationRout == null)
                {

                   chargeAutomationRout = StartCoroutine(ChargeCounterMeasureAutomationRoutine());
                }
                yield return null;
                yield break;
            }

            yield return null;

        }

    }
    private IEnumerator ChargeCounterMeasureAutomationRoutine()
    {
        automationRout = null;
        for (float i = sliderData.automationSlider.value; i < sliderData.automationSlider.maxValue; i += Time.deltaTime)
        {
            sliderController.IncreaseValue(ref sliderData.automationSlider, 0.02f);
            if (sliderData.automationSlider.value >= sliderData.automationSlider.maxValue)
            {
                Debug.Log("Charged");
                chargeAutomationRout = null;
                yield break;
            }
            yield return null;
        }
        yield break;
    }

}
