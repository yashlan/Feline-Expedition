using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField]
    private float soundRadius;
    [SerializeField] 
    private float movementDistance;
    [SerializeField] 
    private int damage; 
    [SerializeField] 
    private float speed; 
    [SerializeField]
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;
    float delay;
    PlayerController _player => PlayerController.Instance;
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    void Update()
    {
        audioSource.mute = GameManager.GameState != GameState.Playing;

        var distance = Vector2.Distance(_player.transform.position, transform.position);

        if(distance < soundRadius)
            audioSource.volume++;
        else
            audioSource.volume--;

        if (movingLeft)
        {
            if(transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingLeft = false;
        }
        else
        {
            if(transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingLeft = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && Time.time > delay)
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

        _player.KnockBack(1200, transform.parent);  
        
        _player.HealthPoint -= (damage - _player.DamageReduction);
        SliderHealthPlayerUI.UpdateUI();

        if (_player.HealthPoint <= 0)
            _player.Dead();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, soundRadius);
    }
}
