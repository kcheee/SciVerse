using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����
public class Orbit : MonoBehaviour
{
    public Transform sunTr;

    [SerializeField] private float period = 0f; // ���� �ֱ�(�ѹ����� ���µ� �ɸ��� �ð�)

    [SerializeField] private float speed = 100f;

    private Vector3 originPos = Vector3.zero;

    private void OnEnable()
    {
        if(originPos != Vector3.zero)
        {
            transform.position = originPos;
        }
    }


    void Start()
    {
        originPos = transform.position;
    }

    void Update()
    {
        RotateAroundSun();
    }

    private void RotateAroundSun()
    {
        // ȸ������ �״�� �ΰ� ��ġ���� �ٲ�. 
        // ������ �ݽõ� �������� ��.
        Quaternion prevRot = transform.rotation;
        transform.RotateAround(sunTr.position, Vector3.up, -(360 / period) * Time.deltaTime * speed);
        transform.rotation = prevRot;
    }
}
