using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWalk : MonoBehaviour
{
    float xController;
    float pos;
    void Update()
    {
        xController = Input.GetAxis("Horizontal");
        transform.Translate(xController * Time.deltaTime * 2, 0, 0);
        //transform.position.x = Mathf.Clamp(transform.position.x, -6.1f, 8);
    }
}
