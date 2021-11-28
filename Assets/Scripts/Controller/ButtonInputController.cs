using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInputController : MonoBehaviour
{
    [SerializeField]
    private Transform _panelOptions;
    [SerializeField]
    private GameObject _panelAssignKey;

    private Text buttonText;
    private Event _keyEvent;
    private KeyCode _newKey;
    private bool _waitingForKey = false;

    #region OnClickEvent
    public void StartAssignmentOnClick(string keyName)
    {
        if (!_waitingForKey)
            StartCoroutine(AssignKey(keyName));
    }

    public void SendTextOnClick(Text text) => buttonText = text;

    public void ResetDefaultInputOnClick()
    {
        OptionsManager.SetDefaultButtonInput();
        LoadButtonText();
    }
    #endregion

    void Start()
    {
        LoadButtonText();
    }

    void OnGUI()
    {
        _keyEvent = Event.current;

        if (_keyEvent.isKey && _waitingForKey && _keyEvent.keyCode != KeyCode.Escape)
        {
            _newKey = _keyEvent.keyCode;
            _waitingForKey = false;
        }
    }

    /// <summary>
    /// button name harus sama dengan nama button gameObject yg ada di inspector
    /// </summary>
    private void SetButtonText(string buttonName, string value)
    {
        for (int i = 0; i < _panelOptions.childCount; i++)
        {
            var child = _panelOptions.GetChild(i);

            if (child.name == buttonName)
                child.GetComponentInChildren<Text>().text = value;                
        }
    }

    private void LoadButtonText()
    {
        SetButtonText("Button Left",         OptionsManager.LeftKey.ToString());
        SetButtonText("Button Right",        OptionsManager.RightKey.ToString());
        SetButtonText("Button Jump",         OptionsManager.JumpKey.ToString());
        SetButtonText("Button Dash",         OptionsManager.DashKey.ToString());
        SetButtonText("Button Throw Attack", OptionsManager.AttackThrowKey.ToString());
        SetButtonText("Button Melee Attack", OptionsManager.AttackMeleeKey.ToString());
        SetButtonText("Button Recharge",     OptionsManager.RechargeKey.ToString());
    }

    private IEnumerator WaitForKey()
    {
        while (!_keyEvent.isKey || _keyEvent.keyCode == KeyCode.Escape)
        {
            _panelAssignKey.SetActive(true);
            yield return null;
        }
    }

    public IEnumerator AssignKey(string keyName)
    {
        _waitingForKey = true;
        yield return WaitForKey();
        switch (keyName)
        {
            case "left":
                OptionsManager.LeftKey = _newKey;
                SetNewKey(OptionsManager.LeftKey.ToString(), PlayerPrefsKey.MOVE_LEFT);
                break;

            case "right":
                OptionsManager.RightKey = _newKey;
                SetNewKey(OptionsManager.RightKey.ToString(), PlayerPrefsKey.MOVE_RIGHT);
                break;

            case "jump":
                OptionsManager.JumpKey = _newKey;
                SetNewKey(OptionsManager.JumpKey.ToString(), PlayerPrefsKey.JUMP);
                break;

            case "dash":
                OptionsManager.DashKey = _newKey;
                SetNewKey(OptionsManager.DashKey.ToString(), PlayerPrefsKey.DASH);
                break;

            case "recharge":
                OptionsManager.RechargeKey = _newKey;
                SetNewKey(OptionsManager.RechargeKey.ToString(), PlayerPrefsKey.RECHARGE);
                break;

            case "melee":
                OptionsManager.AttackMeleeKey = _newKey;
                SetNewKey(OptionsManager.AttackMeleeKey.ToString(), PlayerPrefsKey.ATTACK_MELEE);
                break;

            case "throw":
                OptionsManager.AttackThrowKey = _newKey;
                SetNewKey(OptionsManager.AttackThrowKey.ToString(), PlayerPrefsKey.ATTACK_THROW);
                break;
        }
        yield return null;
    }

    private void SetNewKey(string buttonTextValue, string prefsKey)
    {
        buttonText.text = buttonTextValue;
        OptionsManager.SaveNewKeyCode(prefsKey, buttonText.text);
        _panelAssignKey.SetActive(false);
    }
}
