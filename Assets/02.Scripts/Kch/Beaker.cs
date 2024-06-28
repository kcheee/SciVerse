using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beaker : MonoBehaviour
{
    public int elementIndex;
    [SerializeField] private Element element;

    // 현재 가지고 있는 요소물질
    GameObject elementPowder;

    public bool _isWater = false;
    public bool isAlcohol = false;

    public bool isWater
    {
        get { return _isWater; }
        set
        {
            _isWater = value;
            if (_isWater)
            {
                DissolveInWater();
                Debug.Log("실행");
            }
        }
    }
    // 물을 넣게 된다면, 서서히 사라지게
    private void DissolveInWater()
    {
        Renderer renderer = elementPowder.GetComponent<Renderer>();
        Color originalColor = renderer.material.color;
        float dissolveDuration = 2.0f; // 사라지는 시간 (초)

        renderer.material.DOColor(new Color(originalColor.r, originalColor.g, originalColor.b, 0f), dissolveDuration)
            .OnComplete(() => elementPowder.SetActive(false));
    }


    private void Start()
    {
        element = ElementManager.Instance.GetElement(elementIndex);
        // 자식에 있는 값들 중 Element 오브젝트 찾기
        foreach(Transform child in gameObject.transform)
        {
            if (child.name == element.symbol)
            {
                elementPowder = child.gameObject;
            }
        }
    }


    // 요소 정보
    public void SetElement(Element newElement)
    {
        element = newElement;
        Debug.Log($"Dish received element: {element.symbol}, {element.elementName}, {element.flameColor}");
    }
}
