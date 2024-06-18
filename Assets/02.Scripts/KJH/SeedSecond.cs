using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSecond : MonoBehaviour
{
    [SerializeField] private Range waterRange = new Range(0.2f, 0.5f);
    [SerializeField] private Range lightRange = new Range(0.2f, 0.5f);
    [SerializeField] private Range tempRange = new Range(0.2f, 0.5f);

    [Header("CurAmount")]
    public float waterAmount;
    public float lightAmount;
    public float temperature;
    public float growthProgress { get; set; }

    [Header("Plant Stages")]
    public GameObject seedStage;
    public GameObject smallStage;
    public GameObject mediumStage;
    public GameObject largeStage;
    private GameObject currentStage;

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
        SetStagesInactive();
        SetCondition();
    }

    void Update()
    {
        waterCondition.SetAmount(waterAmount);
        lightCondition.SetAmount(lightAmount);
        temperatureCondition.SetAmount(temperature);

        if (CheckGrowthConditions())
        {
            ActiveGrowthTimer();
        }
        else
        {
            growthTimer = 0;
        }

        UpdatePlantStage();
    }

    void SetStagesInactive()
    {
        seedStage.SetActive(false);
        smallStage.SetActive(false);
        mediumStage.SetActive(false);
        largeStage.SetActive(false);
    }

    void SetCondition()
    {
        growthProgress = 0;
        waterCondition = new Condition(waterRange, "물 부족", "물 과다", "적정");
        lightCondition = new Condition(lightRange, "빛 부족", "빛 과다", "적정");
        temperatureCondition = new Condition(tempRange, "온도 부족", "온도 과다", "적정");
    }

    void Grow()
    {
        float growthRate = 2000f;
        growthProgress += growthRate * Time.deltaTime;
        growthProgress = Mathf.Clamp(growthProgress, 0, 100);
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

    void UpdatePlantStage()
    {
        GameObject newStage = null;

        if (growthProgress <= 25)
        {
            newStage = seedStage;
        }
        else if (growthProgress <= 50)
        {
            newStage = smallStage;
        }
        else if (growthProgress <= 75)
        {
            newStage = mediumStage;
        }
        else
        {
            newStage = largeStage;
        }


        if (currentStage != newStage)
        {
            if (currentStage != null)
            {
                currentStage.SetActive(false);
            }

            newStage.SetActive(true);
            StartCoroutine(LerpScale(newStage.transform, Vector3.one, 1f));
            currentStage = newStage;
        }
    }

    IEnumerator LerpScale(Transform target, Vector3 toScale, float duration)
    {
        Vector3 startScale = target.localScale;
        float time = 0;

        while (time < duration)
        {
            target.localScale = Vector3.Lerp(startScale, toScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        target.localScale = toScale;
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
                waterCondition = new Condition(waterRange, "물 부족", "물 과다", "적정");
                break;
            case "light":
                lightRange = new Range(min, max);
                lightCondition = new Condition(lightRange, "빛 부족", "빛 과다", "적정");
                break;
            case "temperature":
                tempRange = new Range(min, max);
                temperatureCondition = new Condition(tempRange, "온도 부족", "온도 과다", "적정");
                break;
            default:
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
