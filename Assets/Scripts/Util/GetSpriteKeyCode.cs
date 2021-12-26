using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum KeyType
{
    none,
    moveLeft,
    moveRight,
    dash,
    jump,
    attackMelee,
    attackThrow,
    interaction,
    selfheal,
    openMap,
}

public class GetSpriteKeyCode : MonoBehaviour
{
    public KeyType keyType;
    Image image;

    void OnEnable()
    {
        UpdateKey();
    }

    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();
        UpdateKey();
    }

    public void UpdateKey()
    {
        if (keyType == KeyType.attackMelee) image.sprite = GetSprite(PlayerPrefsKey.ATTACK_MELEE);
        if (keyType == KeyType.attackThrow) image.sprite = GetSprite(PlayerPrefsKey.ATTACK_THROW);
        if (keyType == KeyType.dash)        image.sprite = GetSprite(PlayerPrefsKey.DASH);
        if (keyType == KeyType.interaction) image.sprite = GetSprite(PlayerPrefsKey.INTERACTION);
        if (keyType == KeyType.jump)        image.sprite = GetSprite(PlayerPrefsKey.JUMP);
        if (keyType == KeyType.moveLeft)    image.sprite = GetSprite(PlayerPrefsKey.MOVE_LEFT);
        if (keyType == KeyType.moveRight)   image.sprite = GetSprite(PlayerPrefsKey.MOVE_RIGHT);
        if (keyType == KeyType.selfheal)    image.sprite = GetSprite(PlayerPrefsKey.SELFHEAL);
        if (keyType == KeyType.openMap)     image.sprite = GetSprite(PlayerPrefsKey.OPEN_MAP);
    }

    private Sprite GetSprite(string prefsKey) =>
     Resources.Load<Sprite>($"button/{PlayerPrefs.GetString(prefsKey)}");
}
