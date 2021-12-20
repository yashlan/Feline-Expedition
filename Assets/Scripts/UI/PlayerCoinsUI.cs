using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCoinsUI : Singleton<PlayerCoinsUI>
{
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    public static void UpdateUI()
    {
        Instance.text.text = PlayerController.Instance.Coins.ToString();
    }
}
