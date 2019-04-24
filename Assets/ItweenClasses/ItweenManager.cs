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
    //    if (Input.GetKeyDown(KeyCode.H))
    //    {
    //        ItweenMoveTo();
    //    }
    //    if (Input.GetKeyDown(KeyCode.G))
    //    {
    //        ItweenMoveBack(itweenElements[0].startPos, itweenElements[0].itemToTween, itweenElements[0].easeType);
    //    }

    }

    public void ItweenMoveTo(int index)
    {
        iTween.MoveTo(itweenElements[index].itemToTween, iTween.Hash("position", itweenElements[index].endPos, "time", 0.3f, "easetype", itweenElements[index].easeType));
    }
    public void ItweenMoveBack(int index)
    {
        iTween.MoveTo(itweenElements[index].itemToTween, iTween.Hash("position", itweenElements[index].startPos, "time", 0.3f, "easetype", itweenElements[index].easeType));
    }
    public void PunchScaleSlider(Slider slider)
    {
        iTween.PunchScale(slider.gameObject, Vector3.one, 1f);
    }
    public void PunchScaleText(TextMeshProUGUI text)
    {
        iTween.PunchScale(text.gameObject, Vector3.one, 1f);
    }

}
