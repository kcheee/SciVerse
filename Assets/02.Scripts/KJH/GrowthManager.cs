using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrowthManager : MonoBehaviour
{
    public SeedFirst seedFirst;
    public SeedSecond seedSecond;

    [Space(10)]
    [Header("Sliders")]
    public Slider waterSlider;
    public Slider lightSlider;
    public Slider temperatureSlider;

    [Space(10)]
    [Header("Texts")]
    public Text statusText;
    public Text waterStatusText;
    public Text lightStatusText;
    public Text temperatureStatusText;

    [Space(10)]
    [Header("Lights")]
    public Light plantLight; // Light 컴포넌트 연결

    [Space(10)]
    [Header("Watering Can")]
    public GameObject waterCan; // WaterCan 오브젝트 연결
    public float wateringRate = 0.1f; // 물을 줄 때 증가하는 양
    public float decreaseRate = 0.05f; // 물을 주지 않을 때 감소하는 양
    public float maxRotationAngle = 40f; // 최대 회전 각도
    private bool isWatering = false; // 물을 주는 중인지 여부
    private float wateringTime = 0f; // 물을 준 시간

    void Start()
    {
        waterSlider.onValueChanged.AddListener(delegate { seedFirst.SetWaterAmount(waterSlider.value); });
        lightSlider.onValueChanged.AddListener(delegate { seedFirst.SetLightAmount(lightSlider.value); });
        temperatureSlider.onValueChanged.AddListener(delegate { seedFirst.SetTemperature(temperatureSlider.value); });

        waterSlider.onValueChanged.AddListener(delegate { seedSecond.SetWaterAmount(waterSlider.value); });
        lightSlider.onValueChanged.AddListener(delegate { seedSecond.SetLightAmount(lightSlider.value); });
        temperatureSlider.onValueChanged.AddListener(delegate { seedSecond.SetTemperature(temperatureSlider.value); });

        lightSlider.onValueChanged.AddListener(delegate { OnLightIntensityChanged(); });
        temperatureSlider.onValueChanged.AddListener(delegate { OnTemperatureChanged(); });
    }

    void Update()
    {
        // 현재 상태 메세지 업데이트
        OnConditionMessageUpdate();

        // 성장 메세지 업데이트
        OnProcessTextMessageUpdate();

        // 물을 주는 상태 업데이트
        if (isWatering)
        {
            wateringTime += Time.deltaTime;
            IncreaseWaterAmount();
        }
        else
        {
            wateringTime -= Time.deltaTime;
            DecreaseWaterAmount();
        }

        wateringTime = Mathf.Clamp(wateringTime, 0, 1); // 시간 클램프
        UpdateWaterCanRotation(); // 물뿌리개의 회전 업데이트

        // 슬라이더 업데이트
        waterSlider.value = seedFirst.waterAmount;
    }

    void OnConditionMessageUpdate()
    {
        // 씨앗의 성장 상태를 텍스트로 표시
        statusText.text = $"성장: {seedFirst.growthProgress} %";

        // 각 조건에 따른 상태 메시지 업데이트
        waterStatusText.text = $"수분 상태: {seedFirst.GetWaterCondition()}";
        lightStatusText.text = $"빛 상태: {seedFirst.GetLightCondition()}";
        temperatureStatusText.text = $"온도 상태: {seedFirst.GetTemperatureCondition()}";
    }

    void OnProcessTextMessageUpdate()
    {
        // 성장 조건에 따른 전체 상태 메시지 업데이트
        if (seedFirst.CheckGrowthConditions())
        {
            statusText.text += "\n상태: 성장중";
        }
        else
        {
            statusText.text += $"\n상태: 성장 불가 \n 원인: {seedFirst.GetGrowthStatus()}";
        }
    }

    // 빛 변화
    void OnLightIntensityChanged()
    {
        // LightSlider의 값 (0~1)을 Intensity 범위 (0~10)으로 변환
        float intensity = lightSlider.value * 10f;
        plantLight.intensity = intensity;
    }

    void OnTemperatureChanged()
    {
        float temperatureValue = temperatureSlider.value; // TemperatureSlider의 값 (0~1)

        // 적정 온도: 원래 색 (하얀색)
        // 최대 온도: 빨간색 (RGB: 1, 0, 0)
        Color targetColor = Color.Lerp(Color.white, Color.red, temperatureValue);
        plantLight.color = targetColor;
    }

    public void ResetGrowth()
    {
        seedFirst.growthProgress = 0;
        seedFirst.sprout.transform.localScale = Vector3.zero; // 초기화 시 싹의 크기도 초기화
        seedFirst.sprout.SetActive(false); // 초기화 시 싹 비활성화
    }

    void IncreaseWaterAmount()
    {
        seedFirst.waterAmount += wateringRate * Time.deltaTime;
        seedFirst.waterAmount = Mathf.Clamp(seedFirst.waterAmount, 0, 1);
        seedSecond.SetWaterAmount(seedFirst.waterAmount);
    }

    void DecreaseWaterAmount()
    {
        seedFirst.waterAmount -= decreaseRate * Time.deltaTime;
        seedFirst.waterAmount = Mathf.Clamp(seedFirst.waterAmount, 0, 1);
        seedSecond.SetWaterAmount(seedFirst.waterAmount);
    }

    void UpdateWaterCanRotation()
    {
        float angle = Mathf.Lerp(0, maxRotationAngle, wateringTime);
        waterCan.transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    public void StartWatering()
    {
        isWatering = true;
    }

    public void StopWatering()
    {
        isWatering = false;
    }
}
