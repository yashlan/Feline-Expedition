using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    public bool canHeal;
    public bool wasHeal;
    public GameObject canvasInfo;

    // Start is called before the first frame update
    void Start()
    {
        canvasInfo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (canHeal)
        {
            if (Input.GetKeyDown(OptionsManager.InteractionKey) && !wasHeal)
            {
                PanelSlideUIController.Instance.FadeIn(() => StartHeal(), true);
                wasHeal = true;
            }
        }
    }

    private void StartHeal()
    {
        var maxHp = PlayerData.DEFAULT_HEALTHPOINT + PlayerData.HealthPointExtra;
        PlayerController.Instance.HealthPoint = maxHp;

        var maxMp = PlayerData.DEFAULT_MANAPOINT + PlayerData.ManaPointExtra;
        PlayerController.Instance.ManaPoint = maxMp;

        PlayerManaUI.UpdateUI();
        SliderHealthPlayerUI.UpdateUI();

        wasHeal = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canHeal = true;
            canvasInfo.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canHeal = false;
        canvasInfo.SetActive(false);
    }
}
