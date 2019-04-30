using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class ItweenManager : MonoBehaviour
{

    public static ItweenManager instance;

    //public ItweenData itweenData = new ItweenData();
    public iTweenStruct[] itweenElements = null;
    public GameObject[] screenShakeElements = new GameObject[0];
    public Vector3 screenShakeAmount = new Vector3();
    private Coroutine screenShakeRoutine;
    
    [System.Serializable]
    public struct iTweenStruct
    {
        public GameObject itemToTween;
        public Vector3 currentPos;
        public Vector3 startPos;
        public Vector3 endPos;
        public iTween.EaseType easeType;

        public iTweenStruct(Vector3 currentPos, Vector3 startPos, Vector3 endPos, GameObject itemToTween, iTween.EaseType easeType)
        {
            this.itemToTween = itemToTween;
            this.currentPos = currentPos;
            this.startPos = startPos;
            this.endPos = endPos;
            this.easeType = easeType;
        }
    }
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        for (int i = 0; i < itweenElements.Length; i++)
        {
            itweenElements[i].currentPos = itweenElements[i].itemToTween.GetComponent<RectTransform>().position;
        }
    }
    
    public void ItweenMoveTo(int index)
    {
        AudioManager.instance.PlayOneShot(AudioManager.EventType.PanelAcivated);
        iTween.MoveTo(itweenElements[index].itemToTween, iTween.Hash("position", itweenElements[index].endPos, "time", 1, "easetype", itweenElements[index].easeType));
    }
    public void ItweenMoveBack(int index)
    {
        AudioManager.instance.PlayOneShot(AudioManager.EventType.PanelDeactivated);
        iTween.MoveTo(itweenElements[index].itemToTween, iTween.Hash("position", itweenElements[index].startPos, "time", 1, "easetype", itweenElements[index].easeType));
    }
    public void PunchScaleSlider(Slider slider, float speed = 0.3f)
    {
        iTween.PunchScale(slider.gameObject, Vector3.one, speed);
    }
    public void PunchScaleText(TextMeshProUGUI text, float time = 0.3f)
    {
        iTween.PunchScale(text.gameObject, Vector3.one, time);
    }

    public void ScreenShaking()
    {
        for (int i = 0; i < screenShakeElements.Length; i++)
        {
            iTween.PunchPosition(screenShakeElements[i], screenShakeAmount, 0.05f);
        }
    }
}


