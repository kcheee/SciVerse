using UnityEngine;

public class Pipette : MonoBehaviour
{
    public int elementIndex;

    public Element currentElement;

    private void Start()
    {
        // 임의로
        //currentElement = ElementManager.Instance.GetElement(elementIndex);
    }

    private void OnTriggerEnter(Collider other)
    {

        // Beaker to Pipette 
        if (other.gameObject.CompareTag("Beaker"))
        {
            Debug.Log(other.gameObject.GetComponent<Beaker>().elementIndex);
            TransferBeakerToPipette(other.gameObject.GetComponent<Beaker>().elementIndex);
        }

        // Pipette to Dish
        if (other.gameObject.CompareTag("Dish"))
        {
            Debug.Log("요소 옮기기");
            TransferElementToDish(other.GetComponent<Dish>());
        }
    }

    // 스포이드로 가져옴
    public void TransferBeakerToPipette(int index)
    {
        currentElement = ElementManager.Instance.GetElement(index);
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
