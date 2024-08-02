using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Puzzle_Planet : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    enum State
    {
        Idle,
        Move
    }

    private State state = State.Idle;

    private List<Transform> contactedCircleTr = new List<Transform>();

    private Transform prevCircle;
    private Transform myCircle;

    public GameObject nameText;

    private float textYMax = 0f;
    private float textYMin = -50f;

    private Coroutine coroutine = null;

    void Start()
    {
        
    }

    void Update()
    {
        //switch (state)
        //{
        //    case State.Idle:
        //        break;
        //    case State.Move:

        //        //여기부터 
        //        // Screen 좌표계인 mousePosition을 World 좌표계로
        //        //Vector3 mousePos = Input.mousePosition;
        //        //mousePos.z = Mathf.Abs(transform.position.z - Camera.main.transform.position.z);
        //        //mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        //        //transform.position = mousePos;

        //        //if (Input.GetMouseButtonUp(0))
        //        // 여기까지 VR아닐때

        //        if(OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))
        //        {
        //            state = State.Idle;

        //            // 마우스를 놓았을 때 닿아있는 원이 있으면 원 안으로 넣어줌
        //            if (contactedCircleTr.Count > 0)
        //            {
        //                if(myCircle != null)
        //                {
        //                    prevCircle = myCircle;
        //                }

        //                // 닿아있는 원들 중 가장 가까운 원으로 감
        //                myCircle = GetNearestCircle();

        //                // 이미 다른 행성이 있는 자리면 서로 자리 바꿔줌
        //                if(myCircle.childCount > 0)
        //                {
        //                    myCircle.GetComponentInChildren<Puzzle_Planet>().ChangeCircle(prevCircle != null ? prevCircle : transform.parent);
        //                    transform.SetParent(myCircle);
        //                }
        //                else
        //                {
        //                    transform.SetParent(myCircle);
        //                }
        //            }

        //            transform.localPosition = Vector3.zero;
        //        }
        //        break;
        //}
    }

    public void Move(Vector3 pos)
    {
        transform.position = pos;
    }

    public void ReturnOrMove()
    {
        if(contactedCircleTr.Count > 0)
        {
            myCircle =  GetNearestCircle();

            if(myCircle.childCount > 0)
            {
                myCircle.GetComponentInChildren<Puzzle_Planet>().ChangeCircle(prevCircle != null ? prevCircle : transform.parent);
            }

            transform.SetParent(myCircle);
        }

        transform.localPosition = Vector3.zero;
    }

    // 다른 행성과 자리 교체(마우스로 이동한 행성의 스크립트에서 호출해줌)
    public void ChangeCircle(Transform circle)
    {
        myCircle = circle;
        transform.SetParent(circle);
        transform.localPosition = Vector3.zero;
    }

    // 닿아있는 원 중에 가장 가까이 있는 원 찾기
    private Transform GetNearestCircle()
    {
        Transform nearestCircle = null;
        float nearestDist = float.MaxValue;

        foreach (Transform circle in contactedCircleTr)
        {
            float dist = Vector3.Distance(circle.position, transform.position);
            if (nearestCircle == null || nearestDist > dist)
            {
                nearestCircle = circle;
                nearestDist = dist;
            }
        }

        return nearestCircle;
    }

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    if(coroutine != null)
    //    {
    //        StopCoroutine(coroutine);
    //        coroutine = null;
    //    }

    //    coroutine = StartCoroutine(ShowText(true));
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Circle"))
        {
            contactedCircleTr.Add(other.transform);
        }       
        else if (other.CompareTag("Hand"))
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }

            coroutine = StartCoroutine(ShowText(true));

            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
            {
                state = State.Move;
                //transform.SetParent(other.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }

            coroutine = StartCoroutine(ShowText(false));
        }

        contactedCircleTr.Remove(other.transform);
    }

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    if (coroutine != null)
    //    {
    //        StopCoroutine(coroutine);
    //        coroutine = null;
    //    }

    //    coroutine = StartCoroutine(ShowText(false));
    //}

    IEnumerator ShowText(bool isActive)
    {
        Vector3 target = nameText.transform.position;
        RectTransform rect = nameText.GetComponent<RectTransform>();

        nameText.SetActive(true);

        target.y = isActive ? textYMax : textYMin;

        while (Mathf.Abs(rect.anchoredPosition.y - target.y) >= 0.1f)
        {
            rect.anchoredPosition = Vector3.Lerp(rect.anchoredPosition, target, Time.deltaTime * 3f);

            if (!isActive && rect.anchoredPosition.y <= -25f)
            {
                nameText.SetActive(false);
            }

            yield return new WaitForEndOfFrame();

        }

        coroutine = null;
    }

    // VR 아닐때 
    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        state = State.Move;
    //    }
    //}
}
