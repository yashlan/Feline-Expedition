using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInputController : MonoBehaviour
{
    [SerializeField]
    private Transform _panelControl;
    [SerializeField]
    private GameObject _panelAssignKey;

    private Image buttonImage;
    private Event _keyEvent;
    private KeyCode _newKey;
    private bool _waitingForKey = false;

    #region OnClickEvent
    public void StartAssignmentOnClick(string keyName)
    {
        if (!_waitingForKey)
            StartCoroutine(AssignKey(keyName));
    }

    public void SendImageOnClick(Image image) => buttonImage = image;

    public void ResetDefaultInputOnClick()
    {
        OptionsManager.SetDefaultButtonInput();
        LoadButtonSprite();
    }
    #endregion

    void Awake()
    {
        LoadButtonSprite();
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
    private void SetButtonSprite(string buttonName, Sprite value)
    {
        for (int i = 0; i < _panelControl.childCount; i++)
        {
            var button = _panelControl.GetChild(i);

            if (button.name == buttonName)
                button.GetComponent<Image>().sprite = value;                
        }
    }

    private void LoadButtonSprite()
    {
        SetButtonSprite("Button Left",         GetSprite(PlayerPrefsKey.MOVE_LEFT));
        SetButtonSprite("Button Right",        GetSprite(PlayerPrefsKey.MOVE_RIGHT));
        SetButtonSprite("Button Jump",         GetSprite(PlayerPrefsKey.JUMP));
        SetButtonSprite("Button Dash",         GetSprite(PlayerPrefsKey.DASH));
        SetButtonSprite("Button Throw Attack", GetSprite(PlayerPrefsKey.ATTACK_THROW));
        SetButtonSprite("Button Melee Attack", GetSprite(PlayerPrefsKey.ATTACK_MELEE));
        SetButtonSprite("Button Recharge",     GetSprite(PlayerPrefsKey.RECHARGE));
        SetButtonSprite("Button Interaction",  GetSprite(PlayerPrefsKey.INTERACTION));
        SetButtonSprite("Button Open Map",     GetSprite(PlayerPrefsKey.OPEN_MAP));
    }

    private Sprite GetSprite(string prefsKey) =>
         Resources.Load<Sprite>($"button/{PlayerPrefs.GetString(prefsKey)}");

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
                SetNewKey(PlayerPrefsKey.MOVE_LEFT);
                break;

            case "right":
                OptionsManager.RightKey = _newKey;
                SetNewKey(PlayerPrefsKey.MOVE_RIGHT);
                break;

            case "jump":
                OptionsManager.JumpKey = _newKey;
                SetNewKey(PlayerPrefsKey.JUMP);
                break;

            case "dash":
                OptionsManager.DashKey = _newKey;
                SetNewKey(PlayerPrefsKey.DASH);
                break;

            case "recharge":
                OptionsManager.RechargeKey = _newKey;
                SetNewKey(PlayerPrefsKey.RECHARGE);
                break;

            case "melee":
                OptionsManager.AttackMeleeKey = _newKey;
                SetNewKey(PlayerPrefsKey.ATTACK_MELEE);
                break;

            case "throw":
                OptionsManager.AttackThrowKey = _newKey;
                SetNewKey(PlayerPrefsKey.ATTACK_THROW);
                break;

            case "interaction":
                OptionsManager.InteractionKey = _newKey;
                SetNewKey(PlayerPrefsKey.INTERACTION);
                break;

            case "open map":
                OptionsManager.OpenMapKey = _newKey;
                SetNewKey(PlayerPrefsKey.OPEN_MAP);
                break;
        }
        yield return null;
    }

    private void SetNewKey(string prefsKey)
    {
        OptionsManager.SaveNewKeyCode(prefsKey, _newKey.ToString());
        buttonImage.sprite = GetSprite(prefsKey); print(_newKey.ToString());
        _panelAssignKey.SetActive(false);
    }
}
