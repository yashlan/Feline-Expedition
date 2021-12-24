using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    PlayerController _player => PlayerController.Instance;
    EnemyGreenSlime _slime;
    EnemySwordman _swordman;
    PolygonCollider2D _polygonCollider;
    float delay;
    EnemyShieldman _shieldman;



    void Start()
    {
        _polygonCollider = GetComponent<PolygonCollider2D>();

        if(GameManager.SceneType == SceneType.map_1) 
            _slime = GetComponentInParent<EnemyGreenSlime>();
        if(GameManager.SceneType == SceneType.map_3)
             _swordman = GetComponentInParent<EnemySwordman>();
        if(GameManager.SceneType == SceneType.map_3) 
            _shieldman = GetComponentInParent<EnemyShieldman>();
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

        _player.KnockBack(1000, transform.parent);
        
        if(_slime!=null) 
            _player.HealthPoint -= (_slime.Damage - _player.DamageReduction);

        if(_swordman!=null) 
            _player.HealthPoint -= (_swordman.Damage - _player.DamageReduction); 

        if(_shieldman!=null) 
            _player.HealthPoint -= (_shieldman.Damage - _player.DamageReduction);   

        SliderHealthPlayerUI.UpdateUI();

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
