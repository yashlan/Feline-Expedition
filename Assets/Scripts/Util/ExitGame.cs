using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void ExitGameOnClick()
    {
        Application.Quit();

#if UNITY_EDITOR
        print("exit game was executed");
#endif

    } 
}
