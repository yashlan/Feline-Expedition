using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonItemRuneController : MonoBehaviour
{
    public Sprite runeIcon;
    public Text textItemName;
    public Text textPrice;
    public Text textDescription;

    public void SetProperties( 
        Sprite runeIcon,
        string ItemName,
        int Price, 
        string Description)
    {
        this.runeIcon = runeIcon;
        this.textItemName.text = ItemName;
        this.textPrice.text = $"Buy {Price} ";
        this.textDescription.text = Description;
    }
}
