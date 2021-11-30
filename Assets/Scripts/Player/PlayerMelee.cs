using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{

    [SerializeField]
    private GameObject _meleeEffect;

    PolygonCollider2D _polygonCollider;
    GameObject _meleeEffectClone;

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
        _meleeEffectClone = Instantiate(_meleeEffect);
        _meleeEffectClone.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var tag = collision.gameObject.tag;

        if (_tagList.Find(t => t.Equals(tag)) != null)
        {
            TakeDamage(collision);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var tag = collision.gameObject.tag;

        if (_tagList.Find(t => t.Equals(tag)) != null)
        {
            TakeDamage(collision);
        }
    }

    private void TakeDamage(Collision2D collision)
    {
        CameraEffect.PlayShakeEffect();

        if (collision.gameObject.GetComponent<EnemyController>() != null)
        {
            var enemy = collision.gameObject.GetComponent<EnemyController>();

            if (enemy.HealthPoint > 0)
                enemy.HealthPoint -= _player.DamageMelee;
            else
                Destroy(enemy.gameObject);
        }

        if (collision.gameObject.GetComponent<VaseController>() != null)
        {
            var vase = collision.gameObject.GetComponent<VaseController>();

            if (vase.HealthPoint > 0)
                vase.HealthPoint -= _player.DamageMelee;
            else
                Destroy(vase.gameObject);
        }

        var pos = collision.transform.position;
        var randomRotation = Random.Range(-30f, 30f);

        _meleeEffectClone.transform.position = pos;
        _meleeEffectClone.transform.rotation = Quaternion.Euler(0, 0, randomRotation);
        _meleeEffectClone.SetActive(true);
    }

    private void TakeDamage(Collider2D collision)
    {
        CameraEffect.PlayShakeEffect();

        if (collision.gameObject.GetComponent<EnemyController>() != null)
        {
            var enemy = collision.gameObject.GetComponent<EnemyController>();

            if (enemy.HealthPoint > 0)
                enemy.HealthPoint -= _player.DamageMelee;
            else
                Destroy(enemy.gameObject);
        }

        if (collision.gameObject.GetComponent<VaseController>() != null)
        {
            var vase = collision.gameObject.GetComponent<VaseController>();

            if (vase.HealthPoint > 0)
                vase.HealthPoint -= _player.DamageMelee;
            else
                Destroy(vase.gameObject);
        }

        var pos = collision.transform.position;
        var randomRotation = Random.Range(-30f, 30f);

        _meleeEffectClone.transform.position = pos;
        _meleeEffectClone.transform.rotation = Quaternion.Euler(0, 0, randomRotation);
        _meleeEffectClone.SetActive(true);
    }

    #region FOR EVENT ANIMATION

    public void StartMelee() => StartCoroutine(Melee());

    private IEnumerator Melee()
    {
        AudioManager.PlaySfx(AudioManager.Instance.PlayerBasicAttack1Clip);
        _polygonCollider.enabled = true;
        yield return new WaitForSeconds(0.025f);
        _polygonCollider.enabled = false;
        yield break;
    }
    #endregion
}
