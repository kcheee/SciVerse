using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rod : MonoBehaviour
{
    public Transform flameSpawn;

    public GameObject selectedElement;

    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("Flame"))
        {
                Debug.Log("df");
            if (selectedElement != null)
            {
                // GameManager.Instance.ShowElementColour(selectedElement);

                foreach (Transform child in flameSpawn)
                {
                    Destroy(child.gameObject);
                }

                GameObject flame = Instantiate(selectedElement, flameSpawn);
                flame.transform.localPosition = Vector3.zero;
                flame.transform.localRotation = Quaternion.identity;

                selectedElement = null;

            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Flame"))
        {
           // GameManager.Instance.HideElementText();

            foreach (Transform child in flameSpawn)
            {
                child.GetComponent<ParticleSystem>().Stop();
            }
        }
    }
}
