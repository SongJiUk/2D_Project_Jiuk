using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_Fish : Enemy
{

    void Start()
    {
        SetMonster(2f, 0f, 0f, 5f, 3f);
    }


    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRadius)
        {

            TryAttack();

        }
    }

    private void TryAttack()
    {
        anim.SetTrigger("FindPlayer");
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

    protected void Hit(Weapon _weapon)
    {
        HP -= _weapon.DAMAGE;
        if (HP <= 0)
        {
            HP = 0;
            Die();
        }
    }

    public new void Die()
    {

        if (IsexplosionDie)
        {
            anim.SetTrigger("isDie2");
        }
        else
        {
            switch (Random.Range(0, 2))
            {
                case 0:
                    anim.SetTrigger("isDie1");
                    break;

                case 1:
                    anim.SetTrigger("isDie2");
                    break;

            }
        }
        
    }
}
