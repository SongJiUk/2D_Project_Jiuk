using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creature
{
    [SerializeField] protected Player player;
    [SerializeField] protected EStage1Monster monster;
    [SerializeField] protected SpriteRenderer sprite;
    [SerializeField] protected Animator anim;
    public LayerMask enemyLayer;
    protected Vector2 dir = Vector2.zero;

    //몬스터 AI
    protected float detectionRadius;
    protected float attackRange;
    protected float attackCooldown;
    protected float moveSpeed;

    protected float lastAttackTime = 0f;


    //anim
    // isDie
    // Attack1, Attack2
    // ExplosionDie
    // Walk
   
    public void SetMonster(float _hp, float _attackRange, float _attackCoolDown, float _detectionRadius
        , float _moveSpeed)
    {
        //SetMonster(4f, 5f, 3f, 5f);
        HP = _hp;
        attackRange = _attackRange;
        attackCooldown = _attackCoolDown;
        detectionRadius = _detectionRadius;
        moveSpeed = _moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("sdfddsaasdf");
    }
}
 