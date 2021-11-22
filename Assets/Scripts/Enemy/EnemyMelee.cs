using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    PlayerController _player => PlayerController.Instance;
    EnemyController _enemy;
    PolygonCollider2D _polygonCollider; 


    void Start()
    {
        _polygonCollider = GetComponent<PolygonCollider2D>();
        _enemy = GetComponentInParent<EnemyController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        if (_player == null)
            return;

        CameraEffect.PlayShakeEffect();

        _player.PlayHurt();

        if (_player.HealthPoint > 0)
            _player.HealthPoint -= _enemy.Damage; // dikurang damage reduction
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
