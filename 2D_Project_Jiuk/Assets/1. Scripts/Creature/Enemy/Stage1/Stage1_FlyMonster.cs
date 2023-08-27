using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_FlyMonster : Enemy
{
    float attackTimer;
    private void Awake()
    {
        SetMonster(7f, 5f, 4f, 10f, 4f);
        attackTimer = attackCooldown;
    }
    void Update()
    {
        if(!player.ISDIE)
        {
            if (player.transform.position.x < transform.position.x)
            {
                sprite.flipX = false;
                dir = Vector3.left;
            }
            else
            {
                sprite.flipX = true;
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


        if (isFlyingUp)
        {
            transform.Translate(Vector3.up * flyUpSpeed * Time.deltaTime);
            Debug.Log(Vector3.Distance(transform.position, flyUpTargetPosition));
            if (Vector3.Distance(transform.position, flyUpTargetPosition) <= 0.5f)
            {
                isFlyingUp = false;
            }
        }

    }


    private void MoveTowardsPlayer()
    {
        float playerPosX;
        if (sprite.flipX)
        {
            // 플레이어가 오른쪽
            playerPosX = player.transform.position.x + 0.1f;
        }
        else
        {
            //플레이어가 왼쪽
            playerPosX = player.transform.position.x + 0.1f;
        }

        playerPos = new Vector3(playerPosX, player.transform.position.y, player.transform.position.z);
        Vector3 directionToPlayer = (playerPos - transform.position).normalized;


        Vector3 moveAmount = directionToPlayer * moveSpeed * Time.deltaTime;
        Vector3 moveVector = new Vector3(moveAmount.x, 0f, 0f);

        // 이동 실행
        transform.Translate(moveVector);

    }

    private bool isAttacking = false;
    public float attackDuration = 1f;
    private Vector3 flyUpTargetPosition;
    private bool isFlyingUp = false;
    public float flyUpDistance = 3f;
    public float flyUpSpeed = 5f;

    private void TryAttack()
    {

        if (!isAttacking && !isFlyingUp)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                isAttacking = true;
                StartCoroutine(Attack());
                attackTimer = attackCooldown;
            }
        }
    }

  
    private IEnumerator Attack()
    {
        moveSpeed = 6f;
        anim.SetTrigger("Attack1");
        Vector3 attackStartPosition = transform.position;
        Vector3 attackTargetPosition = CalculateAttackTargetPosition();

        float elapsedTime = 0f;

        while (elapsedTime < attackDuration)
        {
            // 부드러운 이동을 위해 Vector3.Lerp를 사용합니다.
            float t = elapsedTime / attackDuration;
            transform.position = Vector3.Lerp(attackStartPosition, attackTargetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        StartFlyUp();
    }
   

    private void StartFlyUp()
    {
        isAttacking = false;
        isFlyingUp = true;

        flyUpTargetPosition = transform.position + (Vector3.up * flyUpDistance);
    }
    private Vector3 CalculateAttackTargetPosition()
    {
        // 부채꼴 모양의 공격 위치를 계산하여 반환합니다.
        Vector3 attackDirection = (transform.position - player.transform.position).normalized;
        float angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg + 90f;
        Quaternion attackRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Vector3 attackPosition = transform.position + attackRotation * Vector3.up * attackRange;

        return attackPosition;
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
