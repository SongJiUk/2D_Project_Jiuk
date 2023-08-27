using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Stage1_Crab : Enemy
{
    [Header("크랩 !!!")]
    [SerializeField] GameObject attack2Obj;
    [SerializeField] Transform attack2_Pos;
    [SerializeField] bool isMeleeAttack;
    [SerializeField] BoxCollider2D seeLeft;
    [SerializeField] BoxCollider2D seeRight;

    
    int RandomNum;
    void Start()
    {
        if(isMeleeAttack) SetMonster(3f, 2f, 2f, 5f, 2f);
        else SetMonster(3f, 5f, 2f, 5f, 2f);
    }

    void Update()
    {

        if(!player.ISDIE)
        {
            if (player.transform.position.x < transform.position.x)
            {
                sprite.flipX = false;
                seeLeft.enabled = true;
                seeRight.enabled = false;
                dir = Vector3.left;
            }
            else
            {
                sprite.flipX = true;
                seeLeft.enabled = false;
                seeRight.enabled = true;
                dir = Vector3.right;
            }

            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= detectionRadius)
            {
                if (distanceToPlayer <= attackRange)
                {

                    TryAttack();
                }
                else
                {

                    MoveTowardsPlayer();
                }
            }
        }

    }


    private void MoveTowardsPlayer()
    {
        anim.SetTrigger("Walk");
        float playerPosX = player.transform.position.x + 0.1f;
        playerPos = new Vector3(playerPosX, player.transform.position.y, player.transform.position.z);
        Vector3 directionToPlayer = (playerPos - transform.position).normalized;
        // 이동 로직 구현 (Translate 또는 Rigidbody 이용)

        Vector3 moveAmount = directionToPlayer * moveSpeed * Time.deltaTime;
        Vector3 moveVector = new Vector3(moveAmount.x, 0f, 0f);

        // 이동 실행
        transform.Translate(moveVector);

    }

    private void TryAttack()
    {

        if (Time.time - lastAttackTime > attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    private void Attack()
    {
        if(isMeleeAttack)anim.SetTrigger("Attack1");
        else anim.SetTrigger("Attack2");
    }

    public void Attack2()
    {
        var attack2 = Instantiate(attack2Obj);
        attack2.transform.position = attack2_Pos.position;
        attack2.GetComponent<Stage1_Crab_Attack2>().SetDir(dir);
    }

    private void OnCollisionEnter2D(Collision2D collision)
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
        StartHitShineParallel();
        if (HP <= 0)
        {
            HP = 0;
            Die();
        }
    }
    //protected void Hit(Weapon _weapon)
    //{
        
    //    if (HP <= 0)
    //    {
    //        HP = 0;
    //        Die();
    //    }
    //}

}
