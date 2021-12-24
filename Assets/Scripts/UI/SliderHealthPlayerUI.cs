using UnityEngine;
using UnityEngine.UI;

public class SliderHealthPlayerUI : Singleton<SliderHealthPlayerUI>
{
    public Slider sliderHP;

    void Start()
    {
        sliderHP.maxValue = PlayerData.DEFAULT_HEALTHPOINT + PlayerData.HealthPointExtra;
    }

    public static void UpdateUI() 
    {
        Instance.sliderHP.value = PlayerController.Instance.HealthPoint;

        if (Instance.sliderHP.value <= 0)
            Instance.sliderHP.value = 0;
    }
}
