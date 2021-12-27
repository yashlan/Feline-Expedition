using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcherMelee : MonoBehaviour
{
    [SerializeField]
    private GameObject _arrowEffect;

    public float _damage;
    public float _speed = 50;
    Rigidbody2D _rb;
    EnemyArcher archer;
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
        archer = FindObjectOfType<EnemyArcher>();
        _rb = GetComponent<Rigidbody2D>();
        _damage = archer.Damage;
    }

    void Start()
    {
        transform.localScale = new Vector3(archer.FacingRight ? 0.75f : -0.75f, 0.75f, 1);
        _rb.velocity = new Vector2(archer.FacingRight ? _speed : -_speed, 0);
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
                    Instantiate(_arrowEffect, transform.position, Quaternion.identity);
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

        player.HealthPoint -= (archer.Damage - player.DamageReduction);

        if (player.HealthPoint <= 0)
            player.Dead();

        SliderHealthPlayerUI.UpdateUI();

        Instantiate(_arrowEffect, collision.transform.position, Quaternion.identity);
    }
}
