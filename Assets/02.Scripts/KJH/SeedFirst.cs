using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Range
{
    public float min;
    public float max;

    public Range(float min, float max)
    {
        this.min = min;
        this.max = max;
    }
}

public class SeedFirst : MonoBehaviour
{
    [SerializeField] private Range waterRange = new Range(0.2f, 0.5f);
    [SerializeField] private Range lightRange = new Range(0.2f, 0.5f);
    [SerializeField] private Range tempRange = new Range(0.2f, 0.5f);

    [Header("CurAmount")]
    public float waterAmount;
    public float lightAmount;
    public float temperature;
    public float growthProgress { get; set; }

    [HideInInspector]
    public GameObject sprout; // ���� Ʈ�� ������Ʈ

    private bool isGrowing;
    private float growthTimer;

    private struct Condition
    {
        public float amount;
        public Range range;
        public string under;
        public string over;
        public string optimal;

        public Condition(Range range, string under, string over, string optimal)
        {
            this.amount = 0; // amount �ʱ�ȭ �߰�
            this.range = range;
            this.under = under;
            this.over = over;
            this.optimal = optimal;
        }

        // �������� Ȯ��
        public bool IsOptimal()
        {
            return amount >= range.min && amount <= range.max;
        }

        // ���� ��ȯ
        public string GetStatus()
        {
            if (amount < range.min)
                return under;
            else if (amount > range.max)
                return over;
            else
                return optimal;
        }

        public void SetAmount(float amount)
        {
            this.amount = amount;
        }
    }

    private Condition waterCondition;
    private Condition lightCondition;
    private Condition temperatureCondition;

    private Material sproutMaterial;
    private Color originalColor = Color.white;
    private Color dryColor = Color.yellow;
    private Color wetColor = Color.blue;

    void Start()
    {
        // �� ����
        SetSprout();

        // ���� �ʱ�ȭ
        SetCondition();

        // Material �� ���� ���� ����
        sproutMaterial = sprout.GetComponent<MeshRenderer>().material;
        originalColor = sproutMaterial.color;
    }

    void Update()
    {
        waterCondition.SetAmount(waterAmount);
        lightCondition.SetAmount(lightAmount);
        temperatureCondition.SetAmount(temperature);

        if (CheckGrowthConditions())
        {
            CheckSproutActive();
            ActiveGrowthTimer();
        }
        else
        {
            growthTimer = 0;
        }

        UpdateSproutColor();
    }

    void SetSprout()
    {
        // ù��° �ڽ� ������Ʈ�� ������ ����
        sprout = transform.GetChild(0).gameObject;
        // ũ�� 0���� �ʱ�ȭ
        sprout.transform.localScale = Vector3.zero;
        // ��Ȱ��ȭ
        sprout.SetActive(false);
    }

    void SetCondition()
    {
        growthProgress = 0;
        waterCondition = new Condition(waterRange, "�� ����", "�� ����", "����");
        lightCondition = new Condition(lightRange, "�� ����", "�� ����", "����");
        temperatureCondition = new Condition(tempRange, "�µ� ����", "�µ� ����", "����");
    }

    void Grow()
    {
        // ���� ����
        float growthRate = 2000f; // ���� �ӵ�
        growthProgress += growthRate * Time.deltaTime; // ���� ���൵ ����

        float scaleFactor = Mathf.Lerp(0f, 1f, growthProgress / 100f); // growthProgress�� 0���� 100 ���̿��� ���� ���� ���¸� ��Ÿ������ ����

        // ���� ũ�⸦ �ε巴�� ������Ŵ
        sprout.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, scaleFactor);
    }

    void ActiveGrowthTimer()
    {
        growthTimer += Time.deltaTime;
        if (growthTimer >= 2)
        {
            Grow();
            growthTimer = 0;
        }
    }


    void CheckSproutActive()
    {
        if (!sprout.activeSelf)
            sprout.SetActive(true);
    }

    void UpdateSproutColor()
    {
        if (!waterCondition.IsOptimal())
        {
            if(waterAmount < waterRange.min)
                sproutMaterial.color = dryColor; // ���� ���� ������ ���� �����
            else
                sproutMaterial.color = wetColor; // ���� ���� ������ ���� �Ķ���

        }
        else
        {
            sproutMaterial.color = originalColor; // ���� ������ ���� ���� ����
        }
    }

    public bool CheckGrowthConditions()
    {
        return waterCondition.IsOptimal() && lightCondition.IsOptimal() && temperatureCondition.IsOptimal();
    }

    public string GetWaterCondition()
    {
        return waterCondition.GetStatus();
    }

    public string GetLightCondition()
    {
        return lightCondition.GetStatus();
    }

    public string GetTemperatureCondition()
    {
        return temperatureCondition.GetStatus();
    }

    public string GetGrowthStatus()
    {
        string status = "";
        if (!waterCondition.IsOptimal())
        {
            status += waterCondition.GetStatus() + " ";
        }
        if (!lightCondition.IsOptimal())
        {
            status += lightCondition.GetStatus() + " ";
        }
        if (!temperatureCondition.IsOptimal())
        {
            status += temperatureCondition.GetStatus() + " ";
        }
        return status.Trim();
    }

    public void SetCondition(string type, float min, float max)
    {
        switch (type)
        {
            case "water":
                waterRange = new Range(min, max);
                waterCondition = new Condition(waterRange, "�� ����", "�� ����", "����");
                break;
            case "light":
                lightRange = new Range(min, max);
                lightCondition = new Condition(lightRange, "�� ����", "�� ����", "����");
                break;
            case "temperature":
                tempRange = new Range(min, max);
                temperatureCondition = new Condition(tempRange, "�µ� ����", "�µ� ����", "����");
                break;
            default:
                Debug.LogWarning("Unknown condition type.");
                break;
        }
    }

    public void SetWaterAmount(float amount)
    {
        waterAmount = amount;
    }

    public void SetLightAmount(float amount)
    {
        lightAmount = amount;
    }

    public void SetTemperature(float temp)
    {
        temperature = temp;
    }
}
