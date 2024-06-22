using TMPro;
using UnityEngine;

public class Dish : MonoBehaviour
{
   [SerializeField] private Element element;

    private void Start()
    {
        element = null;
    }

    private void Update()
    {
        
    }

    // 요소 정보
    public void SetElement(Element newElement)
    {
        element = newElement;
        Debug.Log($"Dish received element: {element.symbol}, {element.elementName}, {element.flameColor}");
        gameObject.GetComponentInChildren<TextMeshPro>().text = element.symbol;
        // 임의로
        //IsBurn();
    }

    // 요소가 있다면 불타게 설정.
    public void IsBurn()
    {
        if (element != null)
        {
            OnBurnParticle();
        }
    }
    public void OnBurnParticle()
    {
        if (element != null)
        {
            Debug.Log(element);
            GameObject EleBurn = Instantiate(element.particlePrefab,transform);

        }
    }
}
