using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public Transform observeCamTr;
    public Transform compareCamTr;
    public Transform[] comparePoints;

    public GameObject observeModeSolarSystem;
    public GameObject compareModeSolarSystem;

    private int comparePlanet = 0;
    private float observeDist = 0f;
    [SerializeField]private float observeMinDist = 100f;
    [SerializeField]private float observeMaxDist = 550f;

    private float speed = 2f;

    private const float zoomUnit = 50f;

    private enum Mode
    {
        ViewAll,
        ViewEach
    }

    private Mode mode = Mode.ViewAll;

    private bool isArrived = true;

    void Start()
    {
        observeDist = observeCamTr.position.y;
        print(observeDist);
    }

    void Update()
    {
        GetInputKey();

        switch (mode)
        {
            case Mode.ViewAll:
                if (!isArrived)
                {
                    ControlDist();
                }
                break;
            case Mode.ViewEach:
                if (!isArrived)
                {
                    ShowPlanet(comparePoints[comparePlanet]);
                }
                break;
        }

    }

    private void GetInputKey()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ChangeMode();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(mode == Mode.ViewEach)
            {
                comparePlanet--;
                if(comparePlanet < 0)
                {
                    comparePlanet = comparePoints.Length - 1;
                }

            }
            else
            {
                observeDist = Mathf.Clamp(observeDist - zoomUnit, observeMinDist, observeMaxDist);
            }

            isArrived = false;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(mode == Mode.ViewEach)
            {
                comparePlanet = ++comparePlanet % comparePoints.Length;
            }
            else
            {
                observeDist = Mathf.Clamp(observeDist + zoomUnit, observeMinDist, observeMaxDist);
            }

            isArrived = false;
        }
    }

    // 모드 전환
    private void ChangeMode()
    {
        mode = mode == Mode.ViewEach ? Mode.ViewAll : Mode.ViewEach;

        observeCamTr.gameObject.SetActive(mode == Mode.ViewAll);
        compareCamTr.gameObject.SetActive(mode == Mode.ViewEach);

        observeModeSolarSystem.SetActive(mode == Mode.ViewAll);
        compareModeSolarSystem.SetActive(mode == Mode.ViewEach);

        switch (mode)
        {
            case Mode.ViewAll:
                observeDist = observeMaxDist;
                observeCamTr.position = new Vector3(0f, observeDist, 0f);
                isArrived = true;
                break;
            case Mode.ViewEach:
                compareCamTr.position = comparePoints[0].position;
                comparePlanet = 0;
                isArrived = false;
                break;
        }

    }

    // 전체관찰 모드 - 관찰할 거리 조절
    private void ControlDist()
    {

        Vector3 v = new Vector3(0, observeDist, 0);

        observeCamTr.position = Vector3.Lerp(observeCamTr.position, v, speed * Time.deltaTime);

        if (Vector3.Distance(observeCamTr.position, v) <= 0.1f)
        {
            isArrived = true;
        }
    }

    // 행성별 관찰 모드 - 행성 줌인
    private void ShowPlanet(Transform dest)
    {
        compareCamTr.position = Vector3.Lerp(compareCamTr.position, dest.position, speed * Time.deltaTime);

        if(Vector3.Distance(compareCamTr.position, dest.position) <= 0.1f)
        {
            isArrived = true;
        }
    }
}
