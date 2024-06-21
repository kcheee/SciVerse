using UnityEngine;

public class Pipette : MonoBehaviour
{
    public int elementIndex;

    private Element currentElement;

    private void Start()
    {
        // 임의로
        currentElement = ElementManager.Instance.GetElement(elementIndex);
    }

    private void OnTriggerEnter(Collider other)
    {

        // Element 가져옴

        // 접시에 옮김.
        if(other.gameObject.CompareTag("Dish"))
        {
            TransferElementToDish(other.GetComponent<Dish>());
        }
    }

    // 스포이드로 가져옴
    public void TransferBeakerToPipette(int index)
    {
        currentElement = ElementManager.Instance.GetElement(elementIndex);
    }

    // 접시에 옮김
    public void TransferElementToDish(Dish dish)
    {
        if (currentElement != null)
        {
            dish.SetElement(currentElement);
        }
    }
}
