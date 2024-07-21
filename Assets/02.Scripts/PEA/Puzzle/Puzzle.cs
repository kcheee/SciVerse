using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour
{
    public GameObject[] planets;
    public Transform[] planetPos;

    public Button checkAnswerBtn;

    public GameObject[] nextParts; // 퍼즐을 맞추면 활성화시켜줘야하는 오브젝트들

    void Start()
    {
        RandomPlanetPos();

        checkAnswerBtn.onClick.AddListener(() => IsAllRight());
    }

    void Update()
    {
        
    }

    private void RandomPlanetPos()
    {
        List<GameObject> planetList = new List<GameObject>(planets);

        for (int i = 0; i < planetPos.Length; i++)
        {
            //planetList[Random.Range(0, planetList.Count - 1)].transform.position = planetPos[i].position;
            GameObject planet = planetList[Random.Range(0, planetList.Count - 1)];
            planet.transform.SetParent(planetPos[i]);
            planet.transform.localPosition = Vector3.zero;
            planetList.Remove(planet);
        }
    }

    // 정답인지 확인 
    public void IsAllRight()
    {
        for (int i = 0; i < planets.Length; i++)
        {
            // 오브젝트 이름에 같은 숫자가 있는지 여부에 따라 정답인지 확인.
            if (planets[i].transform.parent.name.Contains(i.ToString()))
            {
                continue;
            }
            else
            {
                break;
            }
        }

        // 퍼즐 성공.
        foreach(GameObject obj in nextParts)
        {
            obj.SetActive(true);
        }

        gameObject.SetActive(false);
    }
}
