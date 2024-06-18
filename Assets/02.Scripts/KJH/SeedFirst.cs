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
    public GameObject sprout; // 싹이 트는 오브젝트

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
            this.amount = 0; // amount 초기화 추가
            this.range = range;
            this.under = under;
            this.over = over;
            this.optimal = optimal;
        }

        // 적정한지 확인
        public bool IsOptimal()
        {
            return amount >= range.min && amount <= range.max;
        }

        // 상태 반환
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
        // 싹 설정
        SetSprout();

        // 상태 초기화
        SetCondition();

        // Material 및 원래 색상 저장
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
        // 첫번째 자식 오브젝트를 싹으로 설정
        sprout = transform.GetChild(0).gameObject;
        // 크기 0으로 초기화
        sprout.transform.localScale = Vector3.zero;
        // 비활성화
        sprout.SetActive(false);
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
        // 싹의 성장
        float growthRate = 2000f; // 성장 속도
        growthProgress += growthRate * Time.deltaTime; // 성장 진행도 증가

        float scaleFactor = Mathf.Lerp(0f, 1f, growthProgress / 100f); // growthProgress가 0부터 100 사이에서 성장 진행 상태를 나타내도록 설정

        // 싹의 크기를 부드럽게 증가시킴
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
                sproutMaterial.color = dryColor; // 수분 부족 상태일 때는 노란색
            else
                sproutMaterial.color = wetColor; // 수분 과다 상태일 때는 파란색

        }
        else
        {
            sproutMaterial.color = originalColor; // 적정 상태일 때는 원래 색상
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
