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
            SceneManager.LoadScene(Destination);
        }
    }
}
