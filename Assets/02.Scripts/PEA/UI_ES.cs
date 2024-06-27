using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ES : MonoBehaviour
{
    public static UI_ES instance = null;

    public GameObject[] planetInfoPanels;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ShowPlanetInfoPanel(int planetNum = -1)
    {
        foreach (var panel in planetInfoPanels)
        {
            panel.SetActive(planetNum >= 0 ? planetInfoPanels[planetNum] == panel : false);
        }
    }
}
