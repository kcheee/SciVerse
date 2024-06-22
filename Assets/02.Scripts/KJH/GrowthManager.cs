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
    public Light plantLight; // Light ������Ʈ ����

    [Space(10)]
    [Header("Watering Can")]
    public GameObject waterCan; // WaterCan ������Ʈ ����
    public float wateringRate = 0.1f; // ���� �� �� �����ϴ� ��
    public float decreaseRate = 0.05f; // ���� ���� ���� �� �����ϴ� ��
    public float maxRotationAngle = 40f; // �ִ� ȸ�� ����
    private bool isWatering = false; // ���� �ִ� ������ ����
    private float wateringTime = 0f; // ���� �� �ð�

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
        // ���� ���� �޼��� ������Ʈ
        OnConditionMessageUpdate();

        // ���� �޼��� ������Ʈ
        OnProcessTextMessageUpdate();

        // ���� �ִ� ���� ������Ʈ
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

        wateringTime = Mathf.Clamp(wateringTime, 0, 1); // �ð� Ŭ����
        UpdateWaterCanRotation(); // ���Ѹ����� ȸ�� ������Ʈ

        // �����̴� ������Ʈ
        waterSlider.value = seedFirst.waterAmount;
    }

    void OnConditionMessageUpdate()
    {
        // ������ ���� ���¸� �ؽ�Ʈ�� ǥ��
        statusText.text = $"����: {seedFirst.growthProgress} %";

        // �� ���ǿ� ���� ���� �޽��� ������Ʈ
        waterStatusText.text = $"���� ����: {seedFirst.GetWaterCondition()}";
        lightStatusText.text = $"�� ����: {seedFirst.GetLightCondition()}";
        temperatureStatusText.text = $"�µ� ����: {seedFirst.GetTemperatureCondition()}";
    }

    void OnProcessTextMessageUpdate()
    {
        // ���� ���ǿ� ���� ��ü ���� �޽��� ������Ʈ
        if (seedFirst.CheckGrowthConditions())
        {
            statusText.text += "\n����: ������";
        }
        else
        {
            statusText.text += $"\n����: ���� �Ұ� \n ����: {seedFirst.GetGrowthStatus()}";
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
        seedFirst.growthProgress = 0;
        seedFirst.sprout.transform.localScale = Vector3.zero; // �ʱ�ȭ �� ���� ũ�⵵ �ʱ�ȭ
        seedFirst.sprout.SetActive(false); // �ʱ�ȭ �� �� ��Ȱ��ȭ
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
