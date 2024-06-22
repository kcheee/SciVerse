using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beaker : MonoBehaviour
{
    public int elementIndex;
    [SerializeField] private Element element;
    private void Start()
    {
        element = ElementManager.Instance.GetElement(elementIndex);
    }


    // 요소 정보
    public void SetElement(Element newElement)
    {
        element = newElement;
        Debug.Log($"Dish received element: {element.symbol}, {element.elementName}, {element.flameColor}");
    }
}
