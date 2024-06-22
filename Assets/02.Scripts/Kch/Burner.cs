using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burner : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Dish"))
        {
            Debug.Log("�����");
            Dish element = other.gameObject.GetComponent<Dish>();
            if(element != null )
            {
                element.IsBurn();
            }
            else
            {
                Debug.Log("��� ����.");
            }
        }
    }
}
