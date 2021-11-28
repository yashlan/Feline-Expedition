using UnityEngine;

public class BridgeController : MonoBehaviour
{
    public GameObject Bridge;
    float firstEulerZ;

    void Awake()
    {
       firstEulerZ = Bridge.transform.eulerAngles.z;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (Bridge.transform.eulerAngles.z > 0f && Bridge.transform.eulerAngles.z <= firstEulerZ)
            {
                CameraEffect.PlayShakeEffect(3f);
                Bridge.transform.rotation = Quaternion.Euler(0f, 0f, Bridge.transform.eulerAngles.z - 0.15f);
            }
        }
    }
}
