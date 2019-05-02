using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceTimeManager : MonoBehaviour
{
    public SpaceTimeConfig spaceTimeConfig = new SpaceTimeConfig();
    public SpaceTimeData spaceTimeData = new SpaceTimeData();
    public SpaceTimeController spaceTimeController = new SpaceTimeController();

    private void Awake()
    {
        spaceTimeData.distanceText = spaceTimeConfig.distanceText;
    }
    private void Start()
    {

        spaceTimeData.currentDistance = spaceTimeConfig.distanceToGoal;
        spaceTimeData.velocity = spaceTimeConfig.distanceToGoal / spaceTimeConfig.timeToGoal;
        startDecreaseLightyears();
    }

    public void startDecreaseLightyears()
    {
        StartCoroutine(DecreaseLightYears());
    }

    private IEnumerator DecreaseLightYears()
    {
        for(; ; )
        {
            spaceTimeController.CalculateDistance(this, Time.deltaTime, spaceTimeData.velocity);
            spaceTimeData.distanceText.text = "LIGHT YEARS TO GOAL " + spaceTimeData.currentDistance.ToString("0.00");
            if (spaceTimeData.currentDistance < 1)
            {
                GameController.instance.Win();
                yield break;
            }
            //Debug.Log(spaceTimeData.currentDistance.ToString("0.00"));
            yield return null;
        }
    }
}
