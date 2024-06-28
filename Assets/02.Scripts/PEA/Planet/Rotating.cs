using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����
public class Rotating : MonoBehaviour
{
    [SerializeField] private float period = 0f;
    //[SerializeField] private Vector3 rotateAxis = Vector3.zero;

    [SerializeField] private float speed = 100f;  // �����̶� �Ȱ��� ����� ��.
    void Start()
    {
        
    }

    void Update()
    {
        RotateInPlace();
    }

    private void RotateInPlace()
    {
        // �ݽð� �������� ȸ��
        transform.Rotate(Vector3.up, -(360 / period) * Time.deltaTime * speed);
    }
}
