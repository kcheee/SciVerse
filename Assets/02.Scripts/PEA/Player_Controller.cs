using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Controller : MonoBehaviour
{
    //private float rayMaxDist = 10f;
    private RaycastHit hit;
    private GameObject curHitObj;
    private GameObject prevHitObj;

    private LineRenderer lineRenderer;

    public GameObject pointer;
    public Transform rightController;

    public enum InteractionMode
    {
        PointerMode,
        LaserMode
    }

    private InteractionMode interactionMode = InteractionMode.PointerMode;

    void Start()
    {
        SettingLine();
    }

    void Update()
    {
        
    }

    private void SettingLine()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, rightController.position);
        lineRenderer.enabled = false;
    }

    private void ControllerRaycast()
    {

        if(Physics.Raycast(rightController.position, Vector3.forward, out hit) && hit.transform.gameObject.layer == LayerMask.GetMask("UI"))
        {
            if(curHitObj != hit.transform.gameObject)
            {
                prevHitObj = curHitObj;
                curHitObj = hit.transform.gameObject;

                //if(SceneManager.GetActiveScene().buildIndex == 0 && curHitObj.name.Contains("부스"))
                //{
                //    curHitObj.transform.GetChild(0).gameObject.SetActive(true);
                //}
                //else
                //{
                //    if (prevHitObj.name.Contains("부스"))
                //    {
                //        prevHitObj.transform.GetChild(0).gameObject.SetActive(false);
                //    }

                //    //pointer.transform.position = hit.point;
                //    //pointer.SetActive(true);
                //    Pointer(true);
                //}

                switch (interactionMode)
                {
                    case InteractionMode.PointerMode:
                        Pointer(true);
                        break;
                    case InteractionMode.LaserMode:
                        break;
                }
            }
        }
        else
        {
            //if (curHitObj.name.Contains("부스"))
            //{
            //    curHitObj.transform.GetChild(0).gameObject.SetActive(false);
            //}

            //pointer.SetActive(false);
            switch (interactionMode)
            {
                case InteractionMode.PointerMode:
                    Pointer(false);
                    break;
                case InteractionMode.LaserMode:
                    break;
            }

            prevHitObj = curHitObj;
            curHitObj = null;
        }
    } 

    private void Pointer(bool isActive)
    {
        if (isActive)
        {
            pointer.transform.position = hit.point;
        }

        pointer.SetActive(isActive);
    }

    private void Laser(bool isActive)
    {
        if (isActive)
        {
            lineRenderer.SetPosition(1, hit.point);
        }

        lineRenderer.enabled = isActive;
    }

    private void UIInteraction()
    {
        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) && pointer.activeSelf)
        {
            curHitObj.TryGetComponent<Button>(out Button btn);
            btn.onClick.Invoke();
        }
    }
}
