using UnityEngine;

public class Pipette : MonoBehaviour
{
    public int elementIndex;

    private Element currentElement;

    private void Start()
    {
        // ���Ƿ�
        currentElement = ElementManager.Instance.GetElement(elementIndex);
    }

    private void OnTriggerEnter(Collider other)
    {

        // Element ������

        // ���ÿ� �ű�.
        if(other.gameObject.CompareTag("Dish"))
        {
            TransferElementToDish(other.GetComponent<Dish>());
        }
    }

    // �����̵�� ������
    public void TransferBeakerToPipette(int index)
    {
        currentElement = ElementManager.Instance.GetElement(elementIndex);
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
