using UnityEngine;

public class ElementManager : MonoBehaviour
{
    public static ElementManager Instance { get; private set; }

    public ElementData elementData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Element GetElement(int index)
    {
        if (index < 0 || index >= elementData.elements.Length)
        {
            Debug.LogError("Invalid element index");
            return null;
        }
        return elementData.elements[index];
    }
}
