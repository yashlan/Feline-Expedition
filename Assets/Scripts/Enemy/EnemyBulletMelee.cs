using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    Archer,
    Mid_Boss,
}

public class EnemyBulletMelee : MonoBehaviour
{
    [SerializeField]
    private BulletType _bulletType;

    [SerializeField]
    private GameObject _destroyEffect;

    public int _damage;
    public float _speed = 50;
    Rigidbody2D _rb;

    [HideInInspector]
    public EnemyArcher archer;

    [HideInInspector]
    public BossCorrosionSlime midBoss;

    PlayerController player = PlayerController.Instance;

    List<string> _layerList = new List<string>()
    {
        "Player",
        "Vase",
        "Ground",
        "Wall",
    };

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        var scale = transform.localScale;

        if (_bulletType == BulletType.Archer)
        {
            transform.localScale = new Vector3(archer.FacingRight ? scale.x : -scale.x, scale.y, scale.z);
            _rb.velocity = new Vector2(archer.FacingRight ? _speed : -_speed, 0);
        }

        if(_bulletType == BulletType.Mid_Boss)
        {
            transform.localScale = new Vector3(midBoss.FacingRight ? scale.x : -scale.x, scale.y, scale.z);
            _rb.velocity = new Vector2(midBoss.FacingRight ? _speed : -_speed, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var layer = collision.gameObject.layer;

        foreach (var layerName in _layerList)
        {
            if (layer == LayerMask.NameToLayer(layerName))
            {
                if(layerName == "Player")
                    TakeDamage(collision);
                else
                {
                    CameraEffect.PlayShakeEffect(5);
                    Instantiate(_destroyEffect, transform.position, Quaternion.identity);
                }

                Destroy(gameObject);
            }
        }
    }

    private void TakeDamage(Collider2D collision)
    {
        if (player.IsDead)
            return;

        CameraEffect.PlayShakeEffect();

        if (player.IsDefend)
            return;

        player.KnockBack(1000, transform);

        player.HealthPoint -= (_damage - player.DamageReduction);

        if (player.HealthPoint <= 0)
            player.Dead();

        SliderHealthPlayerUI.UpdateUI();

        Instantiate(_destroyEffect, collision.transform.position, Quaternion.identity);
    }
}
