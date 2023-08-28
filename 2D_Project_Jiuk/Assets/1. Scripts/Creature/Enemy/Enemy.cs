using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creature
{
    [SerializeField] protected Player player;
    [SerializeField] protected EStage1Monster monster;
    [SerializeField] protected SpriteRenderer sprite;
    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody2D rigid;
    public LayerMask enemyLayer;
    protected Vector3 dir = Vector3.zero;
    protected bool IsexplosionDie = false;
    protected bool isDie;
    //몬스터 AI
    protected float detectionRadius;
    protected float attackRange;
    protected float attackCooldown;
    protected float moveSpeed;

    protected float lastAttackTime = 0f;


   
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

    private float newAlphaValue = 0.9f;
    private float defaultAlphaValue = 1f;
    Color DefaultColor = Color.white;
    public IEnumerator HitShine()
    {
        Color currentColor = sprite.material.color;
        Color changecolor = new Color(1f, 0.2f, 0.2f);
        int count = 0;
        while (true)
        {
            if (count == 1)
            {
                sprite.material.color = DefaultColor;
                break;
            }

            yield return new WaitForSeconds(0.1f);
            changecolor.a = newAlphaValue;
            sprite.material.color = changecolor;
            yield return new WaitForSeconds(0.1f);
            changecolor.a = defaultAlphaValue;
            sprite.material.color = DefaultColor;
            count++;
        }
    }

    public void StartHitShineParallel()
    {
        StartCoroutine(HitShine());
    }


    public void Die()
    {
        isDie = true;
        rigid.simulated = false;
        anim.SetTrigger("isDie");
        
    }

    public void DestoryOBJ()
    {
        Destroy(gameObject);
    }
}
