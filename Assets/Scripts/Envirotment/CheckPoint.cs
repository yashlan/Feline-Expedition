using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public string CheckPointId;

    void Start()
    {
        PlayerData.Save(PlayerPrefsKey.LAST_CHECKPOINT, CheckPointId);
        print($"data saved, last checkpoint : {PlayerData.LastCheckPoint}");
    }

    void Awake()
    {
        if(PlayerData.LastCheckPoint == CheckPointId)
        {
            PlayerController.Instance.transform.position = transform.position;
        }
    }
}
