using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrowthManager : MonoBehaviour
{
    public Seed seed;
    public Slider waterSlider;
    public Slider lightSlider;
    public Slider temperatureSlider;

    public Text statusText;
    public Text causeText;
    public Text waterStatusText;
    public Text lightStatusText;
    public Text temperatureStatusText;

    public Light plantLight; // Light ������Ʈ ����

    void Start()
    {
        waterSlider.onValueChanged.AddListener(delegate { seed.SetWaterAmount(waterSlider.value); });
        lightSlider.onValueChanged.AddListener(delegate { OnLightIntensityChanged(); });
        temperatureSlider.onValueChanged.AddListener(delegate { OnTemperatureChanged(); });
    }

    void Update()
    {
        // ������ ���� ���¸� �ؽ�Ʈ�� ǥ��
        statusText.text = $"����: {seed.growthProgress} %";

        // �� ���ǿ� ���� ���� �޽��� ������Ʈ
        waterStatusText.text = $"���� ����: {seed.GetWaterCondition()}";
        lightStatusText.text = $"�� ����: {seed.GetLightCondition()}";
        temperatureStatusText.text = $"�µ� ����: {seed.GetTemperatureCondition()}";

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

    void OnLightIntensityChanged()
    {
        float intensity = lightSlider.value * 10f; // LightSlider�� �� (0~1)�� Intensity ���� (0~10)���� ��ȯ
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
        //seed.sprout.transform.localScale = Vector3.zero; // �ʱ�ȭ �� ���� ũ�⵵ �ʱ�ȭ
        //seed.sprout.SetActive(false); // �ʱ�ȭ �� �� ��Ȱ��ȭ
    }
}
