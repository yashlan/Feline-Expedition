using UnityEngine;

public class WaterfallController : MonoBehaviour
{
    ParticleSystem p;
    ParticleSystem.MainModule main;

    void Awake()
    {
        p = GetComponent<ParticleSystem>();
        main = p.main;
        main.simulationSpeed = 20;

        Invoke(nameof(ChangeSimulationSpeed), 0.1f);
    }

    void ChangeSimulationSpeed()
    {
        main.simulationSpeed = 1;
    }
}
