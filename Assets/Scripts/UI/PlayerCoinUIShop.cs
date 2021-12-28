
using UnityEngine.UI;

public class PlayerCoinUIShop : Singleton<PlayerCoinUIShop>
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
