using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCoinsUI : Singleton<PlayerCoinsUI>
{
    Text textCoins;

    void Start()
    {
        textCoins = GetComponent<Text>();
    }

    public static void UpdateUI()
    {
        Instance.textCoins.text = PlayerController.Instance.Coins.ToString();
    }
}
