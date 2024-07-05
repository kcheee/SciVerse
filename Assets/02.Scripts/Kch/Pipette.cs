using UnityEngine;
using UnitySimpleLiquid;

public class Pipette : MonoBehaviour
{
    public int elementIndex;

    public Element currentElement;
    private LiquidContainer liquidContainer;

    private void Start()
    {
        // 임의로
        //currentElement = ElementManager.Instance.GetElement(elementIndex);


        liquidContainer = GetComponentInParent<LiquidContainer>();
    }

    private void OnTriggerEnter(Collider other)
    {

        // Beaker to Pipette 
        if (other.gameObject.CompareTag("Beaker"))
        {
            Debug.Log(other.gameObject.GetComponent<Beaker>().elementIndex);
            TransferBeakerToPipette(other.gameObject.GetComponent<Beaker>().elementIndex);

            // 용액 채우기
            liquidContainer.FillAmountPercent = 1;
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
