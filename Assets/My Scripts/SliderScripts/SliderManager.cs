using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{

    public const int LACK_OF_RESOURCE_MESSAGE = 6;

    public SliderConfig sliderConfig = new SliderConfig();
    public SliderController sliderController = new SliderController();
    public SliderData sliderData = new SliderData();
    public Coroutine automationRout;
    public Coroutine chargeAutomationRout;
    public Coroutine randomEventRoutine;

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
        sliderData.integrityText = sliderConfig.integrityText;
    }
    private void Start()
    {
        if (!TutorialController.instance)
        {
            randomEventRoutine = StartCoroutine(RandomEventRoutine());
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
            StopCoroutine(randomEventRoutine);
        }
    }
    public IEnumerator RandomEventRoutine()
    {
        while (true)
        {
            int random = Random.Range(0, sliderData.structArray.Length);

            yield return new WaitForSeconds(Random.Range(sliderData.minMaxRandomEvent.x, sliderData.minMaxRandomEvent.y));
            ItweenManager.instance.ItweenMoveTo(4);
            yield return new WaitForSeconds(0.5f);
            ItweenManager.instance.ItweenMoveTo(3);
            AudioManager.instance.PlayOneShot(AudioManager.EventType.Warning);
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
            AudioManager.instance.PlayOneShot(AudioManager.EventType.Alarm);

            yield return new WaitForSeconds(1);

            AudioManager.instance.PlayLoop(AudioManager.EventType.ShipIntegrityDamage);
            ItweenManager.instance.ScreenShaking();

            for (float i = 0; i < sliderData.integrityDecreaseDuration; i += Time.deltaTime)
            {
                ItweenManager.instance.ScreenShaking();

                sliderController.ChangeResourceValues(SliderController.InceaseOrDecrease.Decrease, ref sliderData.integritySlider, sliderData.integritySliderDecreaseAmount * Time.deltaTime);
                yield return null;
            }


            AudioManager.instance.TryStopLoop(AudioManager.EventType.ShipIntegrityDamage);
            sliderData.structArray[index].routine = null;

            yield break;
        }

    }
    private IEnumerator DecreaseIntegrityDueToLackOfResourcesRoutine()
    {

        for (; ; )
        {
            sliderController.ChangeResourceValues(SliderController.InceaseOrDecrease.Decrease, ref sliderData.integritySlider, 3 * Time.deltaTime);
            yield return null;
        }


    }
    public void DecreaseIntegrityDueToLackOfResources(Slider slider)
    {

        if (slider.value < 10)
        {
            if (decreaseintegrityDueToLackOfResources == null)
            {
                ItweenManager.instance.ItweenMoveTo(LACK_OF_RESOURCE_MESSAGE);

                sliderData.integrityText.text = "Integrity Taking Damage - Increase Resource";
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
            ItweenManager.instance.ItweenMoveBack(LACK_OF_RESOURCE_MESSAGE);

            decreaseintegrityDueToLackOfResources = null;

        }
    }
    public void StopEvent(int index)
    {
        AudioManager.instance.PlayOneShot(AudioManager.EventType.ButtonSound);
        if (sliderData.structArray[index].routine != null)
        {
            if (sliderData.structArray[index].counterSlider.value >= 30)
            {
                switch (index)
                {
                    case 0:
                        AudioManager.instance.PlayOneShot(AudioManager.EventType.Shields);
                        break;
                    case 1:
                        AudioManager.instance.PlayOneShot(AudioManager.EventType.DrainOxygen);
                        break;
                    case 2:
                        AudioManager.instance.PlayOneShot(AudioManager.EventType.Evasion);
                        break;
                }
                StopCoroutine(sliderData.structArray[index].routine);
                sliderData.structArray[index].routine = null;
                sliderController.ChangeResourceValues(SliderController.InceaseOrDecrease.Decrease, ref sliderData.structArray[index].counterSlider, 30);
                AudioManager.instance.TryStopLoop(AudioManager.EventType.ShipIntegrityDamage);


            }
            else
            {
                sliderData.informationText.text = "INSUFFICIENT RESOURCE";
                AudioManager.instance.PlayOneShot(AudioManager.EventType.NumberFeedbackBad);
                return;
            }
        }
    }
    public void ExchangeResources(int index)
    {
        AudioManager.instance.PlayOneShot(AudioManager.EventType.ButtonSound);
        if (sliderData.structArray[index].counterSlider.value < sliderData.exchangeAmount || sliderData.structArray[index].slider.value + sliderData.exchangeAmount > sliderData.structArray[index].slider.maxValue)
        {

            if (sliderData.structArray[index].counterSlider.value < sliderData.exchangeAmount)
            {
                sliderController.ExchangeResources(ref sliderData.structArray[index].slider, ref sliderData.structArray[index].counterSlider, sliderData.structArray[index].counterSlider.value);

            }
            else if (sliderData.structArray[index].slider.value == sliderData.structArray[index].slider.maxValue)
            {
                return;
            }
            else
            {
                sliderController.ExchangeResources(ref sliderData.structArray[index].slider, ref sliderData.structArray[index].counterSlider, sliderData.structArray[index].slider.maxValue - sliderData.structArray[index].slider.value);
                ItweenManager.instance.PunchScaleSlider(sliderData.structArray[index].slider);
            }
        }
        else
        {
            sliderController.ExchangeResources(ref sliderData.structArray[index].slider, ref sliderData.structArray[index].counterSlider, sliderData.exchangeAmount);
            ItweenManager.instance.PunchScaleSlider(sliderData.structArray[index].slider);
        }
    }
    public void IncreasResourceDirectly(int index, float amount)
    {
        sliderController.ChangeResourceValues(SliderController.InceaseOrDecrease.Increase, ref sliderData.structArray[index].slider, amount);

        if (index != 1)
        {
            numberfeedback?.SpawnText(amount, NumberFeedback.SpawnAtMouseOrObject.Mouse, NumberFeedback.IncreaseOrDecrease.Increase);
            AudioManager.instance.PlayOneShot(AudioManager.EventType.NumberFeedback);
        }
        else
        {
            numberfeedback?.SpawnText(amount, NumberFeedback.SpawnAtMouseOrObject.Object, NumberFeedback.IncreaseOrDecrease.Increase);
            AudioManager.instance.PlayOneShot(AudioManager.EventType.NumberFeedback);
        }
        if (sliderData.structArray[index].slider.value >= sliderData.structArray[index].slider.maxValue)
        {
            return;
        }
    }

    public void DecreaseResourceDirectly(int index, float amount)
    {
        sliderController.ChangeResourceValues(SliderController.InceaseOrDecrease.Decrease, ref sliderData.structArray[index].slider, amount);

        if (index != 1)
        {
            numberfeedback.SpawnText(amount, NumberFeedback.SpawnAtMouseOrObject.Mouse, NumberFeedback.IncreaseOrDecrease.Decrease);
            AudioManager.instance.PlayOneShot(AudioManager.EventType.NumberFeedbackBad);
        }
        else
        {
            numberfeedback.SpawnText(amount, NumberFeedback.SpawnAtMouseOrObject.Object, NumberFeedback.IncreaseOrDecrease.Decrease);
            AudioManager.instance.PlayOneShot(AudioManager.EventType.NumberFeedbackBad);
        }
        if (sliderData.structArray[index].slider.value <= 6)
        {
            Debug.Log("ResourceDepleted");
        }
        if (sliderData.structArray[index].slider.value <= sliderData.structArray[index].slider.minValue)
        {
            return;
        }
    }
    public void ActivateCounterMeasureAutomation()
    {
        AudioManager.instance.PlayOneShot(AudioManager.EventType.ButtonSound);
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
            AudioManager.instance.PlayLoop(AudioManager.EventType.CounterMeasuresAutomated);
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
            
            if (Time.timeScale == 0)
            {
                yield return new WaitUntil(() => Time.timeScale != 0);
            }
            
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
                    AudioManager.instance.TryStopLoop(AudioManager.EventType.CounterMeasuresAutomated);
                    chargeAutomationRout = StartCoroutine(ChargeCounterMeasureAutomationRoutine());
                }

                break;

            }

            yield return null;

        }

        yield break;

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
