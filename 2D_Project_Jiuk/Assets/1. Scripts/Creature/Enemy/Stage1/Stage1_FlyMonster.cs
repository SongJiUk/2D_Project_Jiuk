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

        if (sprite.flipX)   playerPosX = player.transform.position.x + 0.1f;
        else playerPosX = player.transform.position.x + 0.1f;

        playerPos = new Vector3(playerPosX, player.transform.position.y, player.transform.position.z);
        Vector3 directionToPlayer = (playerPos - transform.position).normalized;


        Vector3 moveAmount = directionToPlayer * moveSpeed * Time.deltaTime;
        Vector3 moveVector = new Vector3(moveAmount.x, 0f, 0f);

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
            // �ε巯�� �̵��� ���� Vector3.Lerp�� ����մϴ�.
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
        // ��ä�� ����� ���� ��ġ�� ����Ͽ� ��ȯ�մϴ�.
        Vector3 attackDirection = (transform.position - player.transform.position).normalized;
        float angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg + 90f;
        Quaternion attackRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Vector3 attackPosition = transform.position + attackRotation * Vector3.up * attackRange;

        return attackPosition;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        //������ �� ���ϰ�
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

}
