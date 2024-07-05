using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 공전
public class Orbit : MonoBehaviour
{
    public Transform sunTr;

    [SerializeField] private float period = 0f; // 공전 주기(한바퀴를 도는데 걸리는 시간)

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
        // 회전값은 그대로 두고 위치값만 바꿈. 
        // 공전은 반시뎨 방향으로 함.
        Quaternion prevRot = transform.rotation;
        transform.RotateAround(sunTr.position, Vector3.up, -(360 / period) * Time.deltaTime * speed);
        transform.rotation = prevRot;
    }
}
