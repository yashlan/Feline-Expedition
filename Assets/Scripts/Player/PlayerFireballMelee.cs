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

    List<string> _tagList = new List<string>()
    {
        "Enemy",
        "Vase",
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
        var tag = collision.gameObject.tag;

        if (_tagList.Find(t => t.Equals(tag)) != null)
        {
            TakeDamage(collision);
            Destroy(gameObject);
        }
    }

    private void TakeDamage(Collider2D collision)
    {
        CameraEffect.PlayShakeEffect();

        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            var enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy.IsDead)
            return;

            enemy.HealthPoint -= (_player.DamageMagic - enemy.DamageReduction);

            if (enemy.HealthPoint <= 0 && enemy.enemyType == EnemyType.GreenSlime)
            {
                Destroy(enemy.gameObject);
            }
            else if (enemy.HealthPoint <= 0 && enemy.enemyType == EnemyType.Swordman)
            {
                enemy.Dead();
            }
        }

        if (collision.gameObject.GetComponent<VaseController>() != null)
        {
            var vase = collision.gameObject.GetComponent<VaseController>();

            if (vase.HealthPoint > 0)
                vase.HealthPoint -= _player.DamageMagic;
            else
                Destroy(vase.gameObject);
        }

        Instantiate(_fireballEffect, collision.transform.position, Quaternion.identity);
    }
}