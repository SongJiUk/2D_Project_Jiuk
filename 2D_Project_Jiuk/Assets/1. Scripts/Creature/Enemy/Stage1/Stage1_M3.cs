using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_M3 : Enemy
{
    float attackTimer;
    private void Awake()
    {
        SetMonster(7f, 5f, 4f, 10f, 4f);
        attackTimer = attackCooldown;
    }
}