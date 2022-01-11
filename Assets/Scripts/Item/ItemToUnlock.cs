using UnityEngine;

public enum ItemType
{
    water_spear,
    invincible_shield
}

public class ItemToUnlock : MonoBehaviour
{
    public ItemType itemType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            AudioManager.PlaySfx(null);

            if(itemType == ItemType.water_spear)
                PlayerData.Save(PlayerPrefsKey.WATER_SPEAR, true);

            if(itemType == ItemType.invincible_shield)
                PlayerData.Save(PlayerPrefsKey.INVINCIBLE_SHIELD, true);

            Destroy(gameObject);
        }
    }
}
