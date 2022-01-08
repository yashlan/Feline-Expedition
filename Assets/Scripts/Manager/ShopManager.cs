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
        OptionsManager.ShowMouseCursor();
        PlayerController.Instance.IsShopping = true;
    }

    void OnDisable()
    {
        OptionsManager.HideMouseCursor();
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
            
        }

        if (runeName == "Spell")
        {

        }

        if (runeName == "Power")
        {
            SetupPropertiesItem1(GetIcon("rune_beast"),    "Beast",    50, RuneDescription.BEAST_DESC);
            SetupPropertiesItem2(GetIcon("rune_disorder"), "Disorder", 50, RuneDescription.DISORDER_DESC);
        }

        if (runeName == "Magic")
        {
            SetupPropertiesItem1(GetIcon("rune_ilusion"), "Ilusuion", 50, RuneDescription.ILUSION_DESC);
            SetupPropertiesItem2(GetIcon("rune_truth"),   "Truth",    50, RuneDescription.TRUTH_DESC);
        }

        if (runeName == "Life")
        {
            SetupPropertiesItem1(GetIcon("rune_harmony"), "Harmony", 100, RuneDescription.HARMONY_DESC);
            SetupPropertiesItem2(GetIcon("rune_spirit"),  "Spirit",  100, RuneDescription.SPIRIT_DESC);
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
