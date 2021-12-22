using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float damage; 
    [SerializeField] private float speed; 
    private bool movingleft;
    private float leftEdge;
    private float rightEdge;
    PlayerController _player => PlayerController.Instance;
    PolygonCollider2D _polygonCollider; 

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }
     
    void Update()
    {
        if (movingleft)
        {
            if(transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingleft = false;
        }
        else
        {
            if(transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingleft = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        if (_player.IsDead)
            return;

        CameraEffect.PlayShakeEffect();

        _player.KnockBack(1, transform.parent);  
        SliderHealthPlayerUI.UpdateUI();

        if (_player.HealthPoint <= 0)
            _player.Dead();
    }
}
