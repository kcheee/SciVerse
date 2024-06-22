using UnityEngine;

public class Pipette : MonoBehaviour
{
    public int elementIndex;

    public Element currentElement;

    private void Start()
    {
        // ���Ƿ�
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
            Debug.Log("��� �ű��");
            TransferElementToDish(other.GetComponent<Dish>());
        }
    }

    // �����̵�� ������
    public void TransferBeakerToPipette(int index)
    {
        currentElement = ElementManager.Instance.GetElement(index);
    }

    // ���ÿ� �ű�
    public void TransferElementToDish(Dish dish)
    {
        if (currentElement != null)
        {
            dish.SetElement(currentElement);
        }
    }
}
