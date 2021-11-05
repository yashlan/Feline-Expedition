using UnityEngine;
public enum EnemyType
{
    Easy, Medium, Hard, Expert, BossBattle
}

public class Enemy : MonoBehaviour
{
    [Header("Stat Enemy")]
    public EnemyType EnemyType;
    public int HealthPoint;
    public int Damage;
    public float Speed;
    public float CoolDownAttack;

    public void SetNewStats(EnemyType EnemyType, int HealthPoint, 
        int Damage, float Speed, float CoolDownAttack)
    {
        this.EnemyType = EnemyType;
        this.HealthPoint = HealthPoint;
        this.Damage = Damage;
        this.Speed = Speed;
        this.CoolDownAttack = CoolDownAttack;
        print("new stats excecuted");
    }
}
