
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class CollectFuelController
{

    public GameObject ActivateGoodButton( CollectFuelManager cfm, GameObject button, Color newColor)
    {
        button.GetComponent<Image>().color = newColor;
        UnityEvent click = new UnityEvent();
        click.AddListener(() => cfm.sliderManager.IncreasResourceDirectly((int)cfm.collectFuelConfig.resourceType, cfm.collectFuelConfig.increaseAmt)); // make enum great again
        click.AddListener(() => cfm.NullClickedButton(button));
        click.AddListener(() => AudioManager.instance.PlayOneShot(AudioManager.EventType.NumberFeedback));
        button.GetComponent<CustomButton>().leftClick = click;
        return button;
    }

    public GameObject ActivateBadButton(GameObject button, CollectFuelManager cfm)
    {
        if (button != null)
        {
            UnityEvent badButton = new UnityEvent();
            badButton.AddListener(() => cfm.sliderManager.DecreaseResourceDirectly((int)cfm.collectFuelConfig.resourceType, cfm.collectFuelConfig.increaseAmt));
            button.GetComponent<CustomButton>().leftClick = badButton;
            return button;
        }
        return null;

    }

    public GameObject DeactivateButton(GameObject button, Color defaultcolor)
    {
        UnityEvent rinse = new UnityEvent();

        if (button != null)
        {
            button.GetComponent<CustomButton>().leftClick = rinse;
            button.GetComponent<Image>().color = defaultcolor;
            return button;
        }
        return null;
    }
    
}
