using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ItweenManager : MonoBehaviour
{

    public iTweenStruct[] itweenElements = new iTweenStruct[0];

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
    private void Update()
    {
        for (int i = 0; i < itweenElements.Length; i++)
        {
        itweenElements[i].currentPos = itweenElements[i].itemToTween.GetComponent<RectTransform>().position;
        }
    }




}
