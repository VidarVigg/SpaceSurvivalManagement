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
    private Coroutine decreaseintegrityDueToLackOfResources;
    public GameObject textFeedbackPrefab;
    public NumberFeedback numberfeedback;

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
        sliderData.informationText = sliderConfig.informationText;
        sliderData.automationSlider = sliderConfig.automationSlider;
        sliderData.automated = sliderConfig.automated;
        sliderData.automatedText = sliderConfig.automatedText;
        sliderData.textFeedbackCanvas = sliderConfig.textFeedbackCanvas;
    }
    private void Start()
    {
        if (!TutorialController.instance)
        {
            StartCoroutine(RandomEventRoutine());
        }
    }
    public void SpawnTextFeedback()
    {
        GameObject textClone = Instantiate(textFeedbackPrefab, sliderData.textFeedbackCanvas);
        Destroy(textClone, 1);

    }
    public void CheckIntegrity()
    {
        if (sliderData.integritySlider.value == sliderData.integritySlider.minValue)
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
            ItweenManager.instance.ItweenMoveTo(4);
            yield return new WaitForSeconds(0.5f);
            ItweenManager.instance.ItweenMoveTo(3);
            for (float i = 5; i > 0; i -= Time.deltaTime)
            {
                sliderData.informationText.text = "INCOMING " + sliderData.structArray[random].message + " IN " + i.ToString("0.0");
                yield return null;
            }
            ItweenManager.instance.ItweenMoveBack(4);
            ItweenManager.instance.ItweenMoveTo(5);
            sliderData.informationText.text = sliderData.structArray[random].message;
            ItweenManager.instance.PunchScaleText(sliderData.informationText, 3);
            sliderData.structArray[random].routine = StartCoroutine(EventCoroutine(random));
            yield return new WaitUntil(() => sliderData.structArray[random].routine == null);
            sliderData.informationText.text = null;
            ItweenManager.instance.ItweenMoveBack(3);
            ItweenManager.instance.ItweenMoveBack(5);
        }



    }
    private IEnumerator EventCoroutine(int index)
    {
        if (sliderData.automated == false)
        {
            yield return new WaitForSeconds(1);
            ItweenManager.instance.ScreenShaking();
            for (float i = 0; i < sliderData.integrityDecreaseDuration; i += Time.deltaTime)
            {
                ItweenManager.instance.ScreenShaking();
                sliderController.ChangeResourceValues(SliderController.InceaseOrDecrease.Decrease, ref sliderData.integritySlider, sliderData.integritySliderDecreaseAmount * Time.deltaTime);
                yield return null;
            }

            sliderData.structArray[index].routine = null;
            yield break;
        }

    }
    private IEnumerator DecreaseIntegrityDueToLackOfResourcesRoutine()
    {

        for (; ; )
        {
            sliderController.ChangeResourceValues(SliderController.InceaseOrDecrease.Decrease, ref sliderData.integritySlider, sliderData.integritySliderDecreaseAmount * Time.deltaTime);
            yield return null;
        }


    }
    public void DecreaseIntegrityDueToLackOfResources(Slider slider)
    {
        if (slider.value < 10)
        {
            if (decreaseintegrityDueToLackOfResources == null)
            {

                decreaseintegrityDueToLackOfResources = StartCoroutine(DecreaseIntegrityDueToLackOfResourcesRoutine()); //if not already active
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

        if (decreaseintegrityDueToLackOfResources != null)
        {
            StopCoroutine(decreaseintegrityDueToLackOfResources);

            decreaseintegrityDueToLackOfResources = null;
        }
    }
    public void StopEvent(int index)
    {
        if (sliderData.structArray[index].routine != null)
        {
            if (sliderData.structArray[index].counterSlider.value >= 10)
            {

                StopCoroutine(sliderData.structArray[index].routine);
                sliderData.structArray[index].routine = null;
                sliderController.ChangeResourceValues(SliderController.InceaseOrDecrease.Decrease, ref sliderData.structArray[index].counterSlider, 30);

            }
            else
            {
                sliderData.informationText.text = "INSUFFICIENT RESOURCE";
            }
        }
    }
    public void ExchangeResources(int index)
    {

        if (sliderData.structArray[index].counterSlider.value < sliderData.exchangeAmount || sliderData.structArray[index].slider.value + sliderData.exchangeAmount > sliderData.structArray[index].slider.maxValue)
        {

            if (sliderData.structArray[index].counterSlider.value < sliderData.exchangeAmount)
            {
                sliderController.ExchangeResources(ref sliderData.structArray[index].slider, ref sliderData.structArray[index].counterSlider, sliderData.structArray[index].counterSlider.value );
            }
            else if (sliderData.structArray[index].slider.value == sliderData.structArray[index].slider.maxValue)
            {
                return;
            }
            else
            {
                sliderController.ExchangeResources(ref sliderData.structArray[index].slider, ref sliderData.structArray[index].counterSlider, sliderData.structArray[index].slider.maxValue - sliderData.structArray[index].slider.value );
            }
        }
        else
        {
            sliderController.ExchangeResources(ref sliderData.structArray[index].slider, ref sliderData.structArray[index].counterSlider, sliderData.exchangeAmount);
        }
    }
    public void IncreasResourceDirectly(int index, float amount)
    {
        sliderController.ChangeResourceValues(SliderController.InceaseOrDecrease.Increase, ref sliderData.structArray[index].slider, amount);
        if (sliderData.structArray[index].slider.value >= sliderData.structArray[index].slider.maxValue)
        {
            return;
        }
        if (index != 1)
        {
            numberfeedback?.SpawnText(amount, NumberFeedback.SpawnAtMouseOrObject.Mouse, NumberFeedback.IncreaseOrDecrease.Increase);
        }
        else
        {
            numberfeedback?.SpawnText(amount, NumberFeedback.SpawnAtMouseOrObject.Object, NumberFeedback.IncreaseOrDecrease.Increase);
        }
    }
    public void DecreaseResourceDirectly(int index, float amount)
    {
        sliderController.ChangeResourceValues(SliderController.InceaseOrDecrease.Decrease, ref sliderData.structArray[index].slider, amount);
        if (sliderData.structArray[index].slider.value <= sliderData.structArray[index].slider.minValue)
        {
            return;
        }
        if (index != 1)
        {
            numberfeedback.SpawnText(amount, NumberFeedback.SpawnAtMouseOrObject.Mouse, NumberFeedback.IncreaseOrDecrease.Decrease);
        }
        else
        {
            numberfeedback.SpawnText(amount, NumberFeedback.SpawnAtMouseOrObject.Object, NumberFeedback.IncreaseOrDecrease.Decrease);
        }
        if (sliderData.structArray[index].slider.value <= 6)
        {
            Debug.Log("ResourceDepleted");
        }
    }
    public void ActivateCounterMeasureAutomation()
    {
        if (chargeAutomationRout != null)
        {
            return;
        }
        for (int i = 0; i < sliderData.structArray.Length; i++)
        {
            if (sliderData.structArray[i].routine != null)
            {
                StopEvent(i);
            }
        }
        if (automationRout == null)
        {
            automationRout = StartCoroutine(CounterMesureAutomationRoutine());
        }


    }
    private IEnumerator CounterMesureAutomationRoutine()
    {
        sliderData.automated = true;
        sliderData.automatedText.text = "Counter Measures Automated";
        ItweenManager.instance.PunchScaleText(sliderData.automatedText);
        for (float i = sliderData.automationSlider.value; i > 0; i -= Time.deltaTime)
        {
            sliderController.ChangeResourceValues(SliderController.InceaseOrDecrease.Decrease, ref sliderData.automationSlider, 0.05f);
            if (sliderData.automationSlider.value <= 2)
            {
                sliderData.automated = false;
                Debug.Log("Finished");
                sliderData.automatedText.text = null;
                sliderData.informationText.text = null;
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
            sliderController.ChangeResourceValues(SliderController.InceaseOrDecrease.Increase, ref sliderData.automationSlider, 0.02f);
            if (sliderData.automationSlider.value >= sliderData.automationSlider.maxValue)
            {
                chargeAutomationRout = null;
                yield break;
            }
            yield return null;
        }
        yield break;
    }

}
