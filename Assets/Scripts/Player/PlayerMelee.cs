using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    [SerializeField]
    private GameObject _meleeEffect;

    private float delay;
    PolygonCollider2D _polygonCollider;
    PlayerController _player => PlayerController.Instance;

    List<string> _tagList = new List<string>() 
    { 
        "Enemy", 
        "Vase", 
    };

    void Start()
    {
        _polygonCollider = GetComponentInChildren<PolygonCollider2D>();
        _polygonCollider.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var tag = collision.gameObject.tag;

        if (_tagList.Find(t => t.Equals(tag)) != null && Time.time > delay)
        {
            TakeDamage(collision);
            delay = Time.time + _player.CoolDownAttack;
        }
    }

    public void TakeDamage(Collision2D collision)
    {
        CameraEffect.PlayShakeEffect();

        if (collision.gameObject.GetComponent<EnemyController>() != null)
        {
            var enemy = collision.gameObject.GetComponent<EnemyController>();

            if (enemy.HealthPoint > 0)
                enemy.HealthPoint -= _player.Damage;
            else
                Destroy(enemy.gameObject);
        }

        if (collision.gameObject.GetComponent<VaseController>() != null)
        {
            var vase = collision.gameObject.GetComponent<VaseController>();

            if (vase.HealthPoint > 0)
                vase.HealthPoint -= _player.Damage;
            else
                Destroy(vase.gameObject);
        }

        Instantiate(_meleeEffect, collision.transform.position, Quaternion.identity);
    }

    #region FOR EVENT ANIMATION

    public void StartMelee() => StartCoroutine(Melee());

    private IEnumerator Melee()
    {
        _polygonCollider.enabled = true;
        yield return new WaitUntil(() => !_player.IsAttacking);
        _polygonCollider.enabled = false;
        yield break;
    }
    #endregion
}
