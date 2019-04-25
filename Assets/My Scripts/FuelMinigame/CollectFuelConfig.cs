using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CollectFuelConfig
{
    public Transform gridPanel;
    public GameObject buttonPrefab;
    public GameObject[] buttons = new GameObject[121];
    public Color newColor;
    public Color defaultColor;
    public Canvas collectFuelCanvas;
    public float increaseAmt;
    public CollectFuelManager.ResourceType resourceType;
}
