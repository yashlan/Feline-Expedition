using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    PlayerController _player => PlayerController.Instance;
    EnemyGreenSlime _enemy;
    PolygonCollider2D _polygonCollider;

    float delay;

    void Start()
    {
        _polygonCollider = GetComponent<PolygonCollider2D>();
        _enemy = GetComponentInParent<EnemyGreenSlime>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && Time.time > delay)
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

        _player.HealthPoint -= (_enemy.Damage - _player.DamageReduction); print("enemy hit player");
        SliderHealthPlayerUI.UpdateCurrentHealth();

        if (_player.HealthPoint <= 0)
            _player.Dead();
    }

    #region FOR EVENT ANIMATION

    public void StartMelee() => StartCoroutine(Melee());

    private IEnumerator Melee()
    {
        _polygonCollider.enabled = true;
        yield return new WaitForSeconds(0.025f);
        _polygonCollider.enabled = false;
        yield break;
    }
    #endregion
}
