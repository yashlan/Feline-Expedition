using System.Collections;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    private float delay;

    PolygonCollider2D _polygonCollider;
    PlayerController player => PlayerController.Instance;

    void Start()
    {
        _polygonCollider = GetComponentInChildren<PolygonCollider2D>();
        _polygonCollider.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" && Time.time > delay)
        {
            TakeDamage(collision);
            CameraEffect.PlayShakeEffect();
            print("take damage to enemy excecuted");
            delay = Time.time + player.CoolDownAttack;
        }
    }

    void TakeDamage(Collision2D collision)
    {
        var enemy = collision.gameObject.GetComponent<EnemyController>();
        enemy.HealthPoint -= player.DamageToEnemy;
    }

    #region FOR EVENT ANIMATION

    public void StartMelee()
    {
        StartCoroutine(Melee());
    }

    private IEnumerator Melee()
    {
        _polygonCollider.enabled = true;
        yield return new WaitUntil(() => !player.IsAttacking);
        _polygonCollider.enabled = false;
        yield break;
    }
    #endregion
}
