using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpaceTimeController
{
    
    public void CalculateDistance(SpaceTimeManager stm, float dt, float v)
    {
        stm.spaceTimeData.currentDistance -= v * dt;
    }
    
}
