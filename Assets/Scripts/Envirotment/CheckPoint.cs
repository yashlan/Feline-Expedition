using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public string CheckPointId;

    void Awake()
    {
        if(PlayerData.LastCheckPoint == CheckPointId)
        {
            PlayerController.Instance.transform.position = transform.position;
        }
    }

    void Start()
    {
        PlayerData.Save(PlayerPrefsKey.LAST_CHECKPOINT, CheckPointId);
        print($"data saved, last checkpoint : {PlayerData.LastCheckPoint}");
    }
}
