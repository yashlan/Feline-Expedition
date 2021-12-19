
using UnityEngine;
using UnityEngine.UI;

public class PanelHurtUIController : Singleton<PanelHurtUIController>
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Show()
    {
        anim.SetTrigger("Show");
    }
}
