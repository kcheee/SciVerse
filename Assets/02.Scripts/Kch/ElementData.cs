using UnityEngine;

[CreateAssetMenu(fileName = "ElementData", menuName = "ScriptableObjects/ElementData", order = 1)]
public class ElementData : ScriptableObject
{
    public Element[] elements;
}
