using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    [SerializeField] private Range waterRange = new Range(0.2f, 0.5f);
    [SerializeField] private Range lightRange = new Range(0.2f, 0.5f);
    [SerializeField] private Range tempRange = new Range(0.2f, 0.5f);

    public float waterAmount;
    public float lightAmount;
    public float temperature;
    public float growthProgress { get; set; }

    private GameObject sprout; // ���� Ʈ�� ������Ʈ

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
            this.amount = 0;
            this.range = range;
            this.under = under;
            this.over = over;
            this.optimal = optimal;
        }

        public bool IsOptimal()
        {
            return amount >= range.min && amount <= range.max;
        }

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

    void Start()
    {
        // ù��° �ڽ� ������Ʈ�� ������ ����
        sprout = transform.GetChild(0).gameObject;
        sprout.transform.localScale = Vector3.zero;
        sprout.gameObject.SetActive(false);

        growthProgress = 0;

        waterCondition = new Condition(waterRange, "�� ����", "�� ����", "����");
        lightCondition = new Condition(lightRange, "�� ����", "�� ����", "����");
        temperatureCondition = new Condition(tempRange, "�µ� ����", "�µ� ����", "����");
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
    }

    void Grow()
    {
        // ���� ����
        if (sprout.transform.localScale.x < 1)
        {
            sprout.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            growthProgress += 10;
        }
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
