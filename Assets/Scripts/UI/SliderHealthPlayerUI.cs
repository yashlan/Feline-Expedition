using UnityEngine.UI;

public class SliderHealthPlayerUI : Singleton<SliderHealthPlayerUI>
{
    public Slider sliderHP;
    static PlayerController player => PlayerController.Instance;

    void Start()
    {
        sliderHP.maxValue = player.HealthPoint;
        sliderHP.value = sliderHP.maxValue;
    }

    public static void UpdateCurrentHealth() 
    {
        Instance.sliderHP.value = player.HealthPoint;

        if (Instance.sliderHP.value <= 0)
            Instance.sliderHP.value = 0;
    }
}
