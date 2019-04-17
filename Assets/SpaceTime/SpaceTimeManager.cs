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

        StartCoroutine(Upd8());
    }

    private IEnumerator Upd8()
    {
        for(; ; )
        {
            spaceTimeController.CalculateDistance(this, Time.deltaTime, spaceTimeData.velocity);
            spaceTimeData.distanceText.text = "Light Years to go " + spaceTimeData.currentDistance.ToString("0.00");
            if (spaceTimeData.currentDistance < 1)
            {
                spaceTimeData.distanceText.text = "You Win: You Made it home";
                yield break;
            }
            //Debug.Log(spaceTimeData.currentDistance.ToString("0.00"));
            yield return null;
        }
        yield break;
    }
}
