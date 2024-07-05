using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 자전
public class Rotating : MonoBehaviour
{
    [SerializeField] private float period = 0f;
    //[SerializeField] private Vector3 rotateAxis = Vector3.zero;

    [SerializeField] private float speed = 100f;  // 공전이랑 똑같이 해줘야 함.
    void Start()
    {
        
    }

    void Update()
    {
        RotateInPlace();
    }

    private void RotateInPlace()
    {
        // 반시계 방향으로 회전
        transform.Rotate(Vector3.up, -(360 / period) * Time.deltaTime * speed);
    }
}
