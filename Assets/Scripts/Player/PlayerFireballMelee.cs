using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireballMelee : MonoBehaviour
{
    [SerializeField]
    private GameObject _fireballEffect;

    public float _damage;
    public float _speed = 50;
    Rigidbody2D _rb;
    PlayerController _player => PlayerController.Instance;

    List<string> _layerList = new List<string>()
    {
        "Enemy",
        "Vase",
        "Ground",
        "Wall",
        "Boss Battle",
    };

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _damage = _player.DamageMagic;
    }

    void Start()
    {
        transform.localScale = new Vector3(_player.FacingRight ? 0.65f : -0.65f, 0.65f, 1);
        _rb.velocity = new Vector2(_player.FacingRight ? _speed : -_speed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            Instantiate(_fireballEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        var layer = collision.gameObject.layer;

        foreach (var layerName in _layerList)
        {
            if (layer == LayerMask.NameToLayer(layerName))
            {
                TakeDamage(collision);
                Destroy(gameObject);
            }
        }
    }

    private void TakeDamage(Collider2D collision)
    {
        if (CollisionWithEnemy(collision))
            CameraEffect.PlayShakeEffect();
        else
            CameraEffect.PlayShakeEffect(5);

        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            var enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy.IsDead)
                return;

            if (enemy.IsBlocking)
            {
                Instantiate(_fireballEffect, transform.position, Quaternion.identity);
                return;
            }

            if(!enemy.IsBlocking)
                enemy.HealthPoint -= (_player.DamageMagic - enemy.DamageReduction);

            if (enemy.HealthPoint <= 0)
                enemy.Dead();
        }

        if (collision.gameObject.GetComponent<VaseController>() != null)
        {
            var vase = collision.gameObject.GetComponent<VaseController>();

            if (vase.HealthPoint > 0)
                vase.HealthPoint -= _player.DamageMagic;
            else
                Destroy(vase.gameObject);
        }

        Instantiate(_fireballEffect, CollisionWithEnemy(collision) ? collision.transform.position : transform.position, Quaternion.identity);
    }

    private bool CollisionWithEnemy(Collider2D collision)
    {
        var bossBattle = collision.gameObject.GetComponent<BossCorrosionSlime>();
        return bossBattle == null && collision.gameObject.layer == LayerMask.NameToLayer("Enemy");
    }
}