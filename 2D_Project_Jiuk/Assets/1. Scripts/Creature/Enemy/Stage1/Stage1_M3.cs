using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_M3 : Enemy
{
    float attackTimer;
    int RandNUm;
    public GameObject obj;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //맞으면 색 변하게
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("PlayerAttack")))
        {

            Hit(Player.instace.weapon.DAMAGE);
        }

        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("GrenadeAttack")))
        {
            IsexplosionDie = true;
            Hit(Player.instace.weapon.DAMAGE);
        }

        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Grenade")))
        {
            IsexplosionDie = true;
            Hit(10f);
        }
    }

    protected void Hit(float _damage)
    {
        HP -= _damage;
        if (HP <= 0)
        {
            HP = 0;
            Die();
        }
    }

    public void Die()
    {
        isDie = true;
        //rigid.simulated = false;
        obj.SetActive(false);
        DestoryOBJ();

    }

    public void DestoryOBJ()
    {
        Destroy(gameObject);
    }
}