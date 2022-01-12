using UnityEngine;

public enum ItemType
{
    water_spear,
    invincible_shield
}

public class ItemToUnlock : MonoBehaviour
{
    public GameObject imageInfo;
    public ItemType itemType;

    void Start()
    {
        if(itemType == ItemType.water_spear)
            gameObject.SetActive(!PlayerData.IsWaterSpearUnlocked);

        if (itemType == ItemType.invincible_shield)
            gameObject.SetActive(!PlayerData.IsInvincibleShieldUnlocked);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            AudioManager.PlaySfx(null);

            if(itemType == ItemType.water_spear)
            {
                AudioManager.PlaySfx(null);
                imageInfo.SetActive(true);
                PlayerData.Save(PlayerPrefsKey.WATER_SPEAR, true);
                FloatingTextInfoUI.Show("New Skill Unlocked : Water Spear Melee Attack, Press I To Change", 5f);
            }

            if (itemType == ItemType.invincible_shield)
            {
                AudioManager.PlaySfx(null);
                imageInfo.SetActive(true);
                PlayerData.Save(PlayerPrefsKey.INVINCIBLE_SHIELD, true);
                FloatingTextInfoUI.Show("New Skill Unlocked : Invincible Shield, Press O To Change", 5f);
            }
            Destroy(gameObject);
        }
    }
}
