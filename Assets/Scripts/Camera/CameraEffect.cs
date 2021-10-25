using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraEffect : Singleton<CameraEffect>
{
    [SerializeField]
    private float _shakePower;

    private static float ShakePower => Instance._shakePower;

    static CinemachineBasicMultiChannelPerlin cinemachineBasic;
    // Start is called before the first frame update
    void Start()
    {
        cinemachineBasic = 
            GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }


    public static void PlayShakeEffect()
    {
        Instance.StopAllCoroutines();
        Instance.StartCoroutine(PlayShake(ShakePower));
    }

    private static IEnumerator PlayShake(float power)
    {
        cinemachineBasic.m_AmplitudeGain = power;
        cinemachineBasic.m_FrequencyGain = power;
        yield return new WaitForSeconds(0.25f);
        cinemachineBasic.m_AmplitudeGain = 0f;
        cinemachineBasic.m_FrequencyGain = 0f;
    }
}
