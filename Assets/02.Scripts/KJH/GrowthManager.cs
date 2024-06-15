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
    public Light plantLight; // Light ������Ʈ ����

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
        // ���� ���� �޼��� ������Ʈ
        OnConditionMessageUpdate();

        // ���� �޼��� ������Ʈ
        OnProcessTextMessageUpdate();
    }

    void OnConditionMessageUpdate()
    {
        // ������ ���� ���¸� �ؽ�Ʈ�� ǥ��
        statusText.text = $"����: {seed.growthProgress} %";

        // �� ���ǿ� ���� ���� �޽��� ������Ʈ
        waterStatusText.text = $"���� ����: {seed.GetWaterCondition()}";
        lightStatusText.text = $"�� ����: {seed.GetLightCondition()}";
        temperatureStatusText.text = $"�µ� ����: {seed.GetTemperatureCondition()}";
    }

    void OnProcessTextMessageUpdate()
    {
        // ���� ���ǿ� ���� ��ü ���� �޽��� ������Ʈ
        if (seed.CheckGrowthConditions())
        {
            statusText.text += "\n����: ������";
        }
        else
        {
            statusText.text += $"\n����: ���� �Ұ� \n ����: {seed.GetGrowthStatus()}";
        }
    }

    // �� ��ȭ
    void OnLightIntensityChanged()
    {
        // LightSlider�� �� (0~1)�� Intensity ���� (0~10)���� ��ȯ
        float intensity = lightSlider.value * 10f; 
        plantLight.intensity = intensity;
    }

    void OnTemperatureChanged()
    {
        float temperatureValue = temperatureSlider.value; // TemperatureSlider�� �� (0~1)

        // ���� �µ�: ���� �� (�Ͼ��)
        // �ִ� �µ�: ������ (RGB: 1, 0, 0)
        Color targetColor = Color.Lerp(Color.white, Color.red, temperatureValue);
        plantLight.color = targetColor;
    }

    public void ResetGrowth()
    {
        seed.growthProgress = 0;
        seed.sprout.transform.localScale = Vector3.zero; // �ʱ�ȭ �� ���� ũ�⵵ �ʱ�ȭ
        seed.sprout.SetActive(false); // �ʱ�ȭ �� �� ��Ȱ��ȭ
    }
}
