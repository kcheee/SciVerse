using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCanController : MonoBehaviour
{
    public GrowthManager growthManager;

    void OnMouseDown()
    {
        growthManager.StartWatering();
        print("tt");
    }

    void OnMouseUp()
    {
        growthManager.StopWatering();
    }
}
