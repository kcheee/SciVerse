using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlameTestManager : MonoBehaviour
{
    #region ΩÃ±€≈Ê
    public static FlameTestManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public TextMeshProUGUI ElementSymbol;
    public TextMeshProUGUI ElementName;
    public TextMeshProUGUI ElementColour;


}
