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
    public int DamageToPlayer;
    public int Speed;
    public int CoolDownAttack;

    public void SetNewStats()
    {
        print("new stats excecuted");
    }
}
