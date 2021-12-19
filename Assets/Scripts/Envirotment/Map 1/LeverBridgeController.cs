using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LeverBridgeController : MonoBehaviour
{
    public GameObject Bridge;
    public GameObject canvasInfo;
    public GameObject leverHand;

    bool isSwitched = false;
    Text textInfo;
    Animator anim;
    Animator bridgeAnim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        bridgeAnim = Bridge.GetComponent<Animator>();
        textInfo = canvasInfo.GetComponentInChildren<Text>();

        canvasInfo.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            textInfo.text = $"Press {OptionsManager.InteractionKey} To Switch Lever";

            canvasInfo.SetActive(!isSwitched);

            if(PlayerController.Instance.IsIdle() && Input.GetKeyDown(OptionsManager.InteractionKey) && !isSwitched)
            {
                AudioManager.PlaySfx(AudioManager.Instance.EnviLeverBrigdeClip);
                anim.SetTrigger("Switched");
                isSwitched = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canvasInfo.SetActive(false);
    }

    #region EVENT ANIMATION
    public void RotatingBridge()
    {
        bridgeAnim.SetTrigger("Rotate");
        StartCoroutine(IOnBrigeRotate());
    }

    IEnumerator IOnBrigeRotate()
    {
        while (Bridge.transform.localRotation.z > 0)
        {
            CameraEffect.PlayShakeEffect(4);
            AudioManager.PlaySfx(AudioManager.Instance.EnviBrigdeClip);
            yield return new WaitForSeconds(0.15f);
        }
    }

    #endregion
}
