using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Enemy
{
    [Header("Ability")]
    [SerializeField]
    private bool _canAttack;

    // Start is called before the first frame update
    void Start()
    {
        SetNewStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
