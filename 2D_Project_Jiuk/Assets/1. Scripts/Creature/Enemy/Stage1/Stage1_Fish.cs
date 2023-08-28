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

        Debug.Log(distanceToPlayer);
        if (distanceToPlayer <= detectionRadius)
        {

            TryAttack();

        }
    }

    private void TryAttack()
    {
        anim.SetTrigger("FindPlayer");
    }

    public void MoveToPlayer()
    {
        StartCoroutine(MoveCoroutine());
    }

    IEnumerator MoveCoroutine()
    {
        if(!isDie)
        {
            while (true)
            {
                yield return null;
                transform.position += Vector3.left * Time.deltaTime * moveSpeed;
            }
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

    public new void Die()
    {
        isDie = true;

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
