using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class DebugGame : MonoBehaviour
{
    public Toggle toggleInvicible;
    public Toggle toggleWaterSpear;

    public Slider x_damp;
    public Slider y_damp;
    public Slider off_x;
    public Slider off_y;
    public Text offset_x;
    public Text offset_y;
    public Text damping_x;
    public Text damping_y;

    public Text textInputInfo;
    public Text fpsteks;
    public bool showInputInfo;
    public bool showFPS;
    public float updateInterval = 0.5F;
    public float fps;

    public CinemachineVirtualCamera _camera;
    CinemachineTransposer transposer;

    void Awake()
    {
        transposer = _camera.GetCinemachineComponent<CinemachineTransposer>();
        SetDefault();
    }

    public void SetDefaultOnClick() => SetDefault();

    void SetDefault()
    {
        x_damp.value = 1;
        y_damp.value = 0;

        off_x.value = 0;
        off_y.value = 3;

        transposer.m_FollowOffset =
            new Vector3(off_x.value, off_y.value, transposer.m_FollowOffset.z);

        offset_x.text = $"Offset X : {off_x.value:0.00}";
        offset_y.text = $"Offset Y : {off_y.value:0.00}";

        damping_x.text = $"X Damping : {x_damp.value:0.00}";
        damping_y.text = $"Y Damping : {y_damp.value:0.00}";

        toggleWaterSpear.isOn = PlayerData.IsWaterSpearWasUnlocked;
        toggleInvicible.isOn = PlayerData.IsInvincibleShieldWasUnlocked;
    }

    void Update()
    {
        transposer.m_XDamping = x_damp.value;
        transposer.m_YDamping = y_damp.value;

        transposer.m_FollowOffset = 
            new Vector3(off_x.value, off_y.value, transposer.m_FollowOffset.z);

        offset_x.text = $"Offset X : {off_x.value:0.00}";
        offset_y.text = $"Offset Y : {off_y.value:0.00}";

        damping_x.text = $"X Damping : {x_damp.value:0.00}";
        damping_y.text = $"Y Damping : {y_damp.value:0.00}";

        if (showFPS)
        {
            fps = Time.frameCount / Time.time;
            fpsteks.text = $"FPS : {Mathf.Round(fps)}";
        }

        textInputInfo.gameObject.SetActive(showInputInfo ? true : false);

        toggleInvicible.onValueChanged.AddListener((isOn) => {

            PlayerData.IsInvincibleShieldWasUnlocked = isOn;

        });

        toggleWaterSpear.onValueChanged.AddListener((isOn) => {

            PlayerData.IsWaterSpearWasUnlocked = isOn;

        });
    }

    private void OnGUI()
    {
        GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea);
        guiStyle.alignment = TextAnchor.MiddleCenter;
        guiStyle.fontSize = 25;
        if (GUI.Button(new Rect(Screen.width / 2, Screen.height - 123, 300, 100), "Show Input Info", guiStyle))
        {
            showInputInfo = !showInputInfo;
        }
    }
}
