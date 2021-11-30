using DigitalRuby.RainMaker;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class WeatherEffect : MonoBehaviour
{
    [Header("Dynamic Rain")]
    [SerializeField]
    private bool _useDynamicRainEffect;
    [SerializeField]
    private RainScript2D _rain;
    [SerializeField]
    [Range(0, 1)]
    float _newRainIntensity;

    [Header("Thunder")]
    [SerializeField]
    private bool _useThunderEffect;
    [SerializeField]
    private Light2D _light;

    float _firstIntensity;

    void Start()
    {
        _firstIntensity = _light.intensity;

        if (_useThunderEffect)
            Invoke(nameof(StartThunderEffect), Random.Range(3f, 7f));

        if (_useDynamicRainEffect)
            Invoke(nameof(ChangeRainIntensity), Random.Range(15f, 30f));
    }

    private void ChangeRainIntensity()
    {
        _newRainIntensity = Random.Range(0.5f, 0.6f);
        _rain.RainIntensity = _newRainIntensity;

        var randomTime = Random.Range(15f, 25f);
        Invoke(nameof(ChangeRainIntensity), randomTime);
    }

    private void StartThunderEffect()
    {
        StartCoroutine(ShowThunder(0.05f, 5f));

        var randomTime = Random.Range(15f, 20f);
        Invoke(nameof(StartThunderEffect), randomTime);
    }

    private IEnumerator ShowThunder(float timer, float maxIntensity)
    {
        AudioManager.PlaySfx(AudioManager.Instance.EnviThunderClip);

        _light.intensity = maxIntensity;
        yield return new WaitForSeconds(timer);
        _light.intensity = _firstIntensity;
        yield return new WaitForSeconds(timer);
        _light.intensity = maxIntensity;
        yield return new WaitForSeconds(timer);
        _light.intensity = _firstIntensity;
        yield break;
    }
}
