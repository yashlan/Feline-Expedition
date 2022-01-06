using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public int priceBeast    = 50;
    public int priceDisorder = 50;
    public int priceIlusion  = 50;
    public int priceTruth    = 50;
    public int priceHarmony  = 100;
    public int priceSpirit   = 100;

    private int priceItem;

    public Button buttonMeleeRune;
    public Button buttonSpellRune;
    public Button buttonPowerRune;
    public Button buttonMagicRune;
    public Button buttonLifeRune;

    public Button buttonBuyItem;

    public GameObject bgUIShop;
    public GameObject bgRuneList;

    public Text textRuneName;

    public ButtonItemRuneController itemRune1;
    public ButtonItemRuneController itemRune2;
    public ButtonItemRuneController itemRune3;


    Button buttonItem1;
    Button buttonItem2;
    Button buttonItem3;

    void OnEnable()
    {
        PlayerController.Instance.IsShopping = true;
    }

    void OnDisable()
    {
        PlayerController.Instance.IsShopping = false;
    }

    IEnumerator UpdateCoinsInShop()
    {
        yield return new WaitUntil(() => GameManager.GameState == GameState.Playing);
        PlayerCoinUIShop.UpdateUI();
    }

    void Start()
    {
        buttonItem1 = itemRune1.GetComponent<Button>();
        buttonItem2 = itemRune2.GetComponent<Button>();
        buttonItem3 = itemRune3.GetComponent<Button>();

        buttonMeleeRune.onClick.AddListener(() => ButtonRuneListener("Melee"));
        buttonSpellRune.onClick.AddListener(() => ButtonRuneListener("Spell"));
        buttonPowerRune.onClick.AddListener(() => ButtonRuneListener("Power"));
        buttonMagicRune.onClick.AddListener(() => ButtonRuneListener("Magic"));
        buttonLifeRune.onClick.AddListener(() => ButtonRuneListener("Life"));

        buttonBuyItem.onClick.AddListener(() => ButtonBuyListener());

        StartCoroutine(UpdateCoinsInShop());
    }

    private void ButtonBuyListener()
    {
        if((PlayerController.Instance.Coins - priceItem) >= 0)
        {
            PlayerController.Instance.Coins -= priceItem;

            PlayerCoinUIShop.UpdateUI();
            PlayerCoinsUI.UpdateUI();
        }
        else
        {
            print("not enough");
        }
    }

    void SetPriceItem(int price)
    {
        priceItem = price;
        //print("price : " + priceItem);
    }

    private void ButtonRuneListener(string runeName)
    {
        bgUIShop.SetActive(false);
        bgRuneList.SetActive(true);

        textRuneName.text = $"{runeName} Rune";

        if (runeName == "Melee")
        {
            SetupPropertiesItem1(GetIcon("rune_beast"), "Melee test 1", 10, "Melee test 1 desc");
            SetupPropertiesItem2(GetIcon("rune_disorder"), "Melee test 2", 20, "Melee test 2 desc");
            SetupPropertiesItem3(GetIcon("rune_harmony"), "Melee test 3", 30, "Melee test 3 desc");
        }

        if (runeName == "Spell")
        {
            SetupPropertiesItem1(null, "Spell test 1", 40, "Spell test 1 desc");
            SetupPropertiesItem2(null, "Spell test 2", 50, "Spell test 2 desc");
            SetupPropertiesItem3(null, "Spell test 3", 60, "Spell test 3 desc");
        }

        if (runeName == "Power")
        {
            SetupPropertiesItem1(null, "power test 1", 70, "power test 1 desc");
            SetupPropertiesItem2(null, "power test 2", 80, "power test 2 desc");
            SetupPropertiesItem3(null, "power test 3", 90, "power test 3 desc");
        }

        if (runeName == "Magic")
        {
            SetupPropertiesItem1(null, "Magic test 1", 100, "Magic test 1 desc");
            SetupPropertiesItem2(null, "Magic test 2", 110, "Magic test 2 desc");
            SetupPropertiesItem3(null, "Magic test 3", 120, "Magic test 3 desc");
        }

        if (runeName == "Life")
        {
            SetupPropertiesItem1(null, "Life test 1", 130, "Life test 1 desc");
            SetupPropertiesItem2(null, "Life test 2", 140, "Life test 2 desc");
            SetupPropertiesItem3(null, "Life test 3", 150, "Life test 3 desc");
        }
    }

    void SelectFirstButton() => buttonItem1.Select();

    private void SetupPropertiesItem1(
        Sprite runeIcon, 
        string ItemName, 
        int Price,
        string Description)
    {
        buttonItem1.onClick.RemoveAllListeners();
        itemRune1.SetProperties(runeIcon, ItemName, Price, Description);
        buttonItem1.onClick.AddListener(
            () => 
            {
                itemRune1.SetProperties(runeIcon, ItemName, Price, Description);
                SetPriceItem(Price);
                AudioManager.PlaySfx(AudioManager.Instance.ButtonEnterClip);
            });

        SelectFirstButton();
    }

    private void SetupPropertiesItem2(
        Sprite runeIcon, 
        string ItemName, 
        int Price, 
        string Description)
    {
        buttonItem2.onClick.RemoveAllListeners();
        itemRune2.SetProperties(runeIcon, ItemName, Price, Description);
        buttonItem2.onClick.AddListener(
            () => 
            {          
                itemRune2.SetProperties(runeIcon, ItemName, Price, Description);
                SetPriceItem(Price);
                AudioManager.PlaySfx(AudioManager.Instance.ButtonEnterClip);
            });

        SelectFirstButton();
    }

    private void SetupPropertiesItem3(
        Sprite runeIcon, 
        string ItemName, 
        int Price, 
        string Description)
    {
        buttonItem3.onClick.RemoveAllListeners();
        itemRune3.SetProperties(runeIcon, ItemName, Price, Description);
        buttonItem3.onClick.AddListener(
            () => 
            {
                itemRune3.SetProperties(runeIcon, ItemName, Price, Description);
                SetPriceItem(Price);
                AudioManager.PlaySfx(AudioManager.Instance.ButtonEnterClip);
            });

        SelectFirstButton();
    }

    private Sprite GetIcon(string name) => Resources.Load<Sprite>($"runes/{name}");
}
