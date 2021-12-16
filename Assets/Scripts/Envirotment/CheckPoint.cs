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
}
