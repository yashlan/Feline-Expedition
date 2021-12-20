using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManaUI : Singleton<PlayerManaUI>
{
    Image image;
    float mana;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    public static void UpdateUI()
    {
        Instance.mana = PlayerController.Instance.ManaPoint / 100;
        Instance.image.fillAmount = Instance.mana;
    }
}
