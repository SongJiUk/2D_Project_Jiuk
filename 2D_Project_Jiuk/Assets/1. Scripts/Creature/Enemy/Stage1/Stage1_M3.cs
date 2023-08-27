using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_M3 : Enemy
{
    float attackTimer;
    int RandNUm;
    private void Awake()
    {
        SetMonster(30f, 5f, 4f, 8f, 0f);
        attackTimer = attackCooldown;
    }

    private void Start()
    {
        
    }


    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRadius)
        {
            if (distanceToPlayer <= attackRange)
            {

                TryAttack();
            }
        }
    }

    private void TryAttack()
    {

        if (Time.time - lastAttackTime > attackCooldown)
        {
            RandNUm = Random.Range(0, 2);
            Attack(RandNUm);
            lastAttackTime = Time.time;
        }
    }

    public void Attack(int _num)
    {
        switch(_num)
        {
            case 0:
                anim.SetTrigger("Attack1");
                
                break;
            case 1:
                anim.SetTrigger("Attack2");
                break;
        }
    }

    public void Hit(Weapon _weapon)
    {
        HP -= _weapon.DAMAGE;
        if (HP <= 0)
        {
            HP = 0;
            Die();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //맞으면 색 변하게
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("PlayerAttack")))
        {

            Hit(Player.instace.weapon);
        }

        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("GrenadeAttack")))
        {
            Hit(Player.instace.weapon);
        }
    }
}