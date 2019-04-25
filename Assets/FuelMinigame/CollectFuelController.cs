
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class CollectFuelController
{

    public GameObject ActivateGoodButton( CollectFuelManager cfm, GameObject button, Color newColor)
    {
        button.GetComponent<Image>().color = newColor;
        UnityEvent click = new UnityEvent();
        click.AddListener(() => cfm.test.IncreasResourceDirectly((int)cfm.collectFuelConfig.resourceType, cfm.collectFuelConfig.increaseAmt)); // make enum great again
        click.AddListener(() => cfm.NullClickedButton(button));
        button.GetComponent<CustomButton>().leftClick = click;
        return button;
    }

    public GameObject ActivateBadButton(GameObject button, CollectFuelManager cfm)
    {
        UnityEvent badButton = new UnityEvent();
        badButton.AddListener(() => cfm.test.DecreaseResourceDirectly((int)cfm.collectFuelConfig.resourceType, cfm.collectFuelConfig.increaseAmt));
        button.GetComponent<CustomButton>().leftClick = badButton;
        return button;
    }

    public GameObject DeactivateButton(GameObject button, Color defaultcolor)
    {
        button.GetComponent<Image>().color = defaultcolor;
        UnityEvent rinse = new UnityEvent();

        button.GetComponent<CustomButton>().leftClick = rinse;
        return button;
    }
    
}
