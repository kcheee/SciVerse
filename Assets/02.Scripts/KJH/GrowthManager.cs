using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrowthManager : MonoBehaviour
{
    public Seed seed;

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

    void Start()
    {
        waterSlider.onValueChanged.AddListener(delegate { seed.SetWaterAmount(waterSlider.value); });
        lightSlider.onValueChanged.AddListener(delegate { seed.SetLightAmount(lightSlider.value); });
        temperatureSlider.onValueChanged.AddListener(delegate { seed.SetTemperature(temperatureSlider.value); });

        lightSlider.onValueChanged.AddListener(delegate { OnLightIntensityChanged(); });
        temperatureSlider.onValueChanged.AddListener(delegate { OnTemperatureChanged(); });
    }

    void Update()
    {
        // 현재 상태 메세지 업데이트
        OnConditionMessageUpdate();

        // 성장 메세지 업데이트
        OnProcessTextMessageUpdate();
    }

    void OnConditionMessageUpdate()
    {
        // 씨앗의 성장 상태를 텍스트로 표시
        statusText.text = $"성장: {seed.growthProgress} %";

        // 각 조건에 따른 상태 메시지 업데이트
        waterStatusText.text = $"수분 상태: {seed.GetWaterCondition()}";
        lightStatusText.text = $"빛 상태: {seed.GetLightCondition()}";
        temperatureStatusText.text = $"온도 상태: {seed.GetTemperatureCondition()}";
    }

    void OnProcessTextMessageUpdate()
    {
        // 성장 조건에 따른 전체 상태 메시지 업데이트
        if (seed.CheckGrowthConditions())
        {
            statusText.text += "\n상태: 성장중";
        }
        else
        {
            statusText.text += $"\n상태: 성장 불가 \n 원인: {seed.GetGrowthStatus()}";
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
        seed.growthProgress = 0;
        seed.sprout.transform.localScale = Vector3.zero; // 초기화 시 싹의 크기도 초기화
        seed.sprout.SetActive(false); // 초기화 시 싹 비활성화
    }
}
