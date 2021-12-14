using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionArea : MonoBehaviour
{
    public string Destination;

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // foreach (var scene in SceneManager.GetAllScenes())
            // {
            //     if(scene.name != Destination)
            //     {
            //         Debug.LogWarning($"Scene dengan nama : {Destination} tidak ada, silakan cek lagi");
            //         return;
            //     }
            // }

            SceneManager.LoadScene(Destination);
        }
    }
}
