using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{

    public SliderConfig testConfig = new SliderConfig();
    public SliderController testController = new SliderController();
    public SliderData testData = new SliderData();
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
        testData.structArray = testConfig.structArray;
        testData.minMaxRandomEvent = testConfig.minMaxRandomEvent;
        testData.integritySlider = testConfig.integritySlider;
        testData.integritySliderDecreaseAmount = testConfig.integritySliderDecreaseAmount;
        testData.integrityDecreaseDuration = testConfig.tardymctardface;
        testData.exchangeAmount = testConfig.exchangeAmount;
        testData.text = testConfig.text;
        testData.automationSlider = testConfig.automationSlider;
        testData.automated = testConfig.automated;
        testData.automatedText = testConfig.automatedText;
    }
    private void Start()
    {
        StartCoroutine(RandomEventRoutine());
    }
    public void CheckIntegrity()
    {
        if (testData.integritySlider.value <= testConfig.integrityLoseAmt)
        {
           testData.text.text = GameController.instance.GameOver();

        }
    }

    private IEnumerator RandomEventRoutine()
    {


        while (true)
        {
            int random = Random.Range(0, testData.structArray.Length);
            yield return new WaitForSeconds(Random.Range(testData.minMaxRandomEvent.x, testData.minMaxRandomEvent.y));
            for (float i = 5; i > 1; i -= Time.deltaTime)
            {
                testData.text.text = "Incoming " + testData.structArray[random].message + " in " + i.ToString("0.0");
                yield return null;
            }
            testData.structArray[random].routine = StartCoroutine(EventCoroutine(random));
            testData.text.text = testData.structArray[random].message;
            yield return new WaitUntil(() => testData.structArray[random].routine == null);
            testData.text.text = null;

        }



    }
    private IEnumerator EventCoroutine(int index)
    {
        if (testData.automated == false)
        {
            for (float i = 0; i < testData.integrityDecreaseDuration; i += Time.deltaTime)
            {
                testController.DecreaseValue(ref testData.integritySlider, testData.integritySliderDecreaseAmount * Time.deltaTime);
                yield return null;
            }
            testData.structArray[index].routine = null;
            yield break;
        }

    }
    private IEnumerator DecreaseIntegrityDueToLackOfResourcesRoutine()
    {

            for (; ;)
            {
                testController.DecreaseValue(ref testData.integritySlider, testData.integritySliderDecreaseAmount * Time.deltaTime);
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

        for (int i = 0; i < testData.structArray.Length; i++)
        {
            if (testData.structArray[i].slider.value < 10)
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
        if (testData.structArray[index].routine != null)
        {
            if (testData.structArray[index].counterSlider.value >= 20)
            {
                
                StopCoroutine(testData.structArray[index].routine);
                testData.structArray[index].routine = null;
                testController.DecreaseValue(ref testData.structArray[index].counterSlider, 20);

            }
            else
            {
                testData.text.text = "Insufficient Resource!!1!!111 1 1!";
            }
        }
    }

    public void ExchangeResources(int index)
    {

        if (testData.structArray[index].counterSlider.value < testData.exchangeAmount || testData.structArray[index].slider.value + testData.exchangeAmount > testData.structArray[index].slider.maxValue)
        {

            if (testData.structArray[index].counterSlider.value < testData.exchangeAmount)
            {
                testController.ExchangeResources(ref testData.structArray[index].slider, ref testData.structArray[index].counterSlider, testData.structArray[index].counterSlider.value);
            }

            else
            {
                testController.ExchangeResources(ref testData.structArray[index].slider, ref testData.structArray[index].counterSlider, testData.structArray[index].slider.maxValue - testData.structArray[index].slider.value);
            }

        }
        else
        {
            testController.ExchangeResources(ref testData.structArray[index].slider, ref testData.structArray[index].counterSlider, testData.exchangeAmount);
        }

    }

    public void IncreasResourceDirectly(int index, float amount)
    {
        testController.IncreaseValue(ref testData.structArray[index].slider, amount);

    }
    public void DecreaseResourceDirectly(int index, float amount)
    {
        testController.DecreaseValue(ref testData.structArray[index].slider, amount);
        if (testData.structArray[index].slider.value <= 6)
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
        testData.automated = true;
        testData.automatedText.text = "Counter Measures Automated";
        for (float i = testData.automationSlider.value; i > 0; i -= Time.deltaTime)
        {
            testController.DecreaseValue(ref testData.automationSlider, 0.05f);
            if (testData.automationSlider.value <= 2)
            {
                testData.automated = false;
                Debug.Log("Finished");
                testData.automatedText.text = null;
                testData.text.text = null;
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
        for (float i = testData.automationSlider.value; i < testData.automationSlider.maxValue; i += Time.deltaTime)
        {
            testController.IncreaseValue(ref testData.automationSlider, 0.02f);
            if (testData.automationSlider.value >= testData.automationSlider.maxValue)
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
