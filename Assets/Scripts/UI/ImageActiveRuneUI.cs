using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum RuneType
{
    none,
    melee,
    spell
}

public class ImageActiveRuneUI : MonoBehaviour
{
    private const string RUNE_FIREBALL          = "rune_fireball";
    private const string RUNE_INVINCIBLE_SHIELD = "rune_invincible_shield";

    private const string RUNE_MELEE             = "rune_melee";
    private const string RUNE_WATER_SPEAR       = "rune_water_spear";


    public RuneType runeType;
    public GameObject imageInfo;
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();

        LoadSprite();
    }

    void LoadSprite()
    {
        if (runeType == RuneType.melee)
        {
            image.sprite = GetSprite(
                PlayerData.IsWaterSpearUsed() ?
                RUNE_WATER_SPEAR : RUNE_MELEE);

            imageInfo.SetActive(PlayerData.IsWaterSpearUnlocked);
        }

        if (runeType == RuneType.spell)
        {
            image.sprite = GetSprite(
                PlayerData.IsInvincibleShieldUsed() ?
                RUNE_INVINCIBLE_SHIELD : RUNE_FIREBALL);

            imageInfo.SetActive(PlayerData.IsInvincibleShieldUnlocked);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.GameState == GameState.Playing)
        {
            if(runeType == RuneType.melee && PlayerData.IsWaterSpearUnlocked)
            {
                if (Input.GetKeyDown(KeyCode.I))
                {
                    if(image.sprite.name == RUNE_MELEE)
                        PlayerData.Save(PlayerPrefsKey.WATER_SPEAR_EQUIP, true);

                    if (image.sprite.name == RUNE_WATER_SPEAR)
                        PlayerData.Save(PlayerPrefsKey.WATER_SPEAR_EQUIP, false);

                    LoadSprite();
                }
            }

            if (runeType == RuneType.spell && PlayerData.IsInvincibleShieldUnlocked)
            {
                if (Input.GetKeyDown(KeyCode.O))
                {
                    if (image.sprite.name == RUNE_FIREBALL)
                        PlayerData.Save(PlayerPrefsKey.INVINCIBLE_SHIELD_EQUIP, true);


                    if (image.sprite.name == RUNE_INVINCIBLE_SHIELD)
                        PlayerData.Save(PlayerPrefsKey.INVINCIBLE_SHIELD_EQUIP, false);

                    LoadSprite();
                }
            }
        }
    }

    private Sprite GetSprite(string name) => Resources.Load<Sprite>($"runes/active/{name}");
}
