using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beaker : MonoBehaviour
{
    public int elementIndex;
    [SerializeField] private Element element;

    // ���� ������ �ִ� ��ҹ���
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
                Debug.Log("����");
            }
        }
    }
    // ���� �ְ� �ȴٸ�, ������ �������
    private void DissolveInWater()
    {
        Renderer renderer = elementPowder.GetComponent<Renderer>();
        Color originalColor = renderer.material.color;
        float dissolveDuration = 2.0f; // ������� �ð� (��)

        renderer.material.DOColor(new Color(originalColor.r, originalColor.g, originalColor.b, 0f), dissolveDuration)
            .OnComplete(() => elementPowder.SetActive(false));
    }


    private void Start()
    {
        element = ElementManager.Instance.GetElement(elementIndex);
        // �ڽĿ� �ִ� ���� �� Element ������Ʈ ã��
        foreach(Transform child in gameObject.transform)
        {
            if (child.name == element.symbol)
            {
                elementPowder = child.gameObject;
            }
        }
    }


    // ��� ����
    public void SetElement(Element newElement)
    {
        element = newElement;
        Debug.Log($"Dish received element: {element.symbol}, {element.elementName}, {element.flameColor}");
    }
}
