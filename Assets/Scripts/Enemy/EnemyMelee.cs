using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MeleeType
{
    GreenSlime,
    SwordMan,
    ShieldMan,
    MidBossSlime,
}

public class EnemyMelee : MonoBehaviour
{
    public MeleeType meleeType;
    PlayerController _player => PlayerController.Instance;

    PolygonCollider2D _polygonCollider;
    float delay;

    EnemyShieldman _shieldman;
    EnemyGreenSlime _slime;
    EnemySwordman _swordman;
    BossCorrosionSlime _midBossSlime;

    void Start()
    {
        _polygonCollider = GetComponent<PolygonCollider2D>();

        SetupMeleeType();
    }

    private void SetupMeleeType()
    {
        if(meleeType == MeleeType.GreenSlime) 
            _slime = GetComponentInParent<EnemyGreenSlime>();

        if (meleeType == MeleeType.MidBossSlime)
            _midBossSlime = GetComponentInParent<BossCorrosionSlime>();

        if (meleeType == MeleeType.ShieldMan)
            _shieldman = GetComponentInParent<EnemyShieldman>();

        if (meleeType == MeleeType.SwordMan)
            _swordman = GetComponentInParent<EnemySwordman>();
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

        GetDamageFrom(meleeType);

        SliderHealthPlayerUI.UpdateUI();

        if (_player.HealthPoint <= 0)
            _player.Dead();
    }

    private void GetDamageFrom(MeleeType type)
    {
        if(type == MeleeType.GreenSlime)
            _player.HealthPoint -= (_slime.Damage - _player.DamageReduction);

        if (type == MeleeType.MidBossSlime)
            _player.HealthPoint -= (_midBossSlime.Damage - _player.DamageReduction);

        if (type == MeleeType.ShieldMan)
            _player.HealthPoint -= (_shieldman.Damage - _player.DamageReduction);

        if (type == MeleeType.SwordMan)
            _player.HealthPoint -= (_swordman.Damage - _player.DamageReduction);
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
