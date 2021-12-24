using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] 
    private float movementDistance;
    [SerializeField] 
    private int damage; 
    [SerializeField] 
    private float speed; 
    private bool movingleft;
    private float leftEdge;
    private float rightEdge;
    float delay;
    PlayerController _player => PlayerController.Instance;
    PolygonCollider2D _polygonCollider; 

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }
     
    void Start()
    {
        _polygonCollider = GetComponent<PolygonCollider2D>();
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
        if(collision.gameObject.tag == "Player"  && Time.time > delay )
        {
            TakeDamage();
            delay = Time.time + 0.5f;
        }
    }

    private void TakeDamage()
    {
        if (_player.IsDead)
            return;

        CameraEffect.PlayShakeEffect();

        _player.KnockBack(1, transform.parent);  
        
        _player.HealthPoint -= (damage - _player.DamageReduction);
        SliderHealthPlayerUI.UpdateUI();

        if (_player.HealthPoint <= 0)
            _player.Dead();
    }
}
