using UnityEngine;

public class Dish : MonoBehaviour
{
   [SerializeField] private Element element;

    public void SetElement(Element newElement)
    {
        element = newElement;
        Debug.Log($"Dish received element: {element.symbol}, {element.elementName}, {element.flameColor}");
    }
}
