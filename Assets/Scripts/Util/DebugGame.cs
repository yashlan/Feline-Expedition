using UnityEngine;
using UnityEngine.UI;

public class DebugGame : MonoBehaviour
{
    public Text fpsteks;
    public bool showFPS;
    public float updateInterval = 0.5F;
    public float fps;

    void Update()
    {
        if (showFPS)
        {
            fps = Time.frameCount / Time.time;
            fpsteks.text = "FPS : " + Mathf.Round(fps);
        }
    }
}
