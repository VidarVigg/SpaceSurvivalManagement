using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberFeedback : MonoBehaviour
{
    public GameObject feedbackTextPrefab;
    public Queue<TextMeshProUGUI> textQueue = new Queue<TextMeshProUGUI>();
    public Transform textFeedbackCanvas;
    public Vector3 spawnPos;
    public Transform spawnposObject;
    private string plusOrMinus;
    
    public enum SpawnAtMouseOrObject
    {
        Mouse,
        Object,
    }
    public enum IncreaseOrDecrease
    {
        Increase,
        Decrease,
    }


    public void SpawnText(float value, SpawnAtMouseOrObject spawnmode, IncreaseOrDecrease increaseOrDecrease)
    {
        if (spawnmode == SpawnAtMouseOrObject.Mouse)
        {
            spawnPos = Input.mousePosition + Vector3.up * 10f;
        }
        else if (spawnmode == SpawnAtMouseOrObject.Object)
        {
            spawnPos = spawnposObject.position;
        }
        if (increaseOrDecrease == IncreaseOrDecrease.Increase)
        {
            plusOrMinus = "+";
        }
        else if (increaseOrDecrease == IncreaseOrDecrease.Decrease)
        {
            plusOrMinus = "-";
        }
        GameObject textPrefabClone = Instantiate(feedbackTextPrefab, spawnPos, Quaternion.identity, textFeedbackCanvas);
        TextMeshProUGUI text = textPrefabClone.GetComponent<TextMeshProUGUI>();
        text.text = plusOrMinus + ((int)value).ToString();
        ItweenManager.instance.PunchScaleText(text);
        textQueue.Enqueue(text);       
        StartCoroutine(DeleteText());

    }

    private IEnumerator DeleteText()
    {
        TextMeshProUGUI tempText = textQueue.Dequeue();

        for (float i = 1; i >= 0; i-=Time.deltaTime)
        {
            tempText.color-= new Color(0, 0, 0, Time.deltaTime);
            yield return null;
        }
        Destroy(tempText.gameObject);
        yield break;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            
        }
    }

}
