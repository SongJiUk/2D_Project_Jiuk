using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Player : Creature
{
    public static Player instace = null;
    
    Vector2 movement = Vector2.zero;
    Vector2 dir = Vector2.zero;
    Vector3 bDir = Vector3.right;
    Vector2 left = new Vector2(-1f, 1f);
    Vector2 right = new Vector2(1f, 1f);
    Vector3 raycast = new Vector3(0, 0.5f, 0);
    Vector3 LastPos = Vector3.zero;

    float speed = 3f;
    bool isShoot = false;
    bool isJump = false;
    bool isSit = false;
    bool isLookUp = false;
    bool isLookDown = false;
    bool isGetHw = false;
    bool isMelee = false;
    bool isDie = false;

    int Life = 2;
    public int LIFE
    {
        get { return Life; }
        set { Life = value; }
    }

    public bool ISDIE
    {
        get { return isDie; }
    }

    [Header("캐릭터 관련")]
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] GameObject[] CharacterBody;
    [SerializeField] BoxCollider2D UpperCollider;
    [SerializeField] BoxCollider2D AllBodyCollider;
    

    [Header("캐릭터 애니메이션 관련")]
    [SerializeField] Animator UpperAnim;
    [SerializeField] Animator LowerAnim;
    [SerializeField] Animator AllBodyAnim;

    [Header("총 발사 관련")]
    [SerializeField] GameObject[] firePosObj;
    [SerializeField] GameObject[] BulletObj;
    public Weapon weapon;
    Vector3 Firepos = Vector3.zero;

    private bool canShoot = true;

    Vector3 bulletLeft = new Vector3(0f, 180f, 0f);
    float RandomNum;
    public LayerMask enemyLayer;
    Transform pos;
    string WeaponName = "Default";


    private void Awake()
    {
        if (null == instace) instace = this;
        else Destroy(this.gameObject);


        dir = Vector2.right;
        isGetHw = false;
    }

    
    void CharacterChange(bool isOneObj)
    {
        if(isOneObj)
        {
            CharacterBody[0].SetActive(false);
            CharacterBody[1].SetActive(false);
            CharacterBody[2].SetActive(true);

        }
        else
        {
            CharacterBody[0].SetActive(true);
            CharacterBody[1].SetActive(true);
            CharacterBody[2].SetActive(false);
        }
    }
    private void Start()
    {
        weapon.DefaultWeapon();

    }


    void Respawn()
    {
        rigid.simulated = true;
        transform.position = LastPos + new Vector3(0f, 3f, 0f);
    }

    void Die()
    {
        isDie = true;
        rigid.simulated = false;
        CharacterChange(true);
        AllBodyAnim.SetTrigger("IsDie");
        Life--;
        LastPos = transform.position;
        if (Life == 0)
        {
            //다시 시작 
        }
        else
        {
            Respawn();
        }
    }

    

    public void ChangeWeapon(Weapon _weapon)
    {
        weapon = _weapon;
        WeaponName = weapon.eWeapon.ToString();

        if(weapon.eWeapon.Equals(EWeaponName.Default)) isGetHw = false;
        else isGetHw = true;
       
        UpperAnim.SetBool("IsGetHw", isGetHw);
        AllBodyAnim.SetBool("IsGetHw", isGetHw);

        

        UpperAnim.SetBool("IsHeavyMachinGun", weapon.ISHEAVYMACHINGUN);
    }

    void HandleInput()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

    }

    void UpdateAnimations()
    {

        if (isGetHw) UpperAnim.SetBool("IsGetHw", isGetHw);
        else WeaponName = EWeaponName.Default.ToString();

        if (movement.x < 0)
        {
            transform.rotation = Quaternion.Euler(bulletLeft);
            dir = Vector2.left * 3f;
            bDir = Vector3.left;
        }
        else if (movement.x > 0)
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
            dir = Vector2.right * 3f;
            bDir = Vector3.right;
        }


       //아래 보는 경우
        if (movement.y < 0)
        {
            //점프중에 아래 볼경우
            if (isJump)
            {
                isSit = false;
                isLookDown = true;
                UpperAnim.SetBool("LookDown", isLookDown);
            }
            else
            {
                isSit = true;
                isLookDown = false;
            }
        }
        //위로 볼 경우
        else if (movement.y > 0)
        {
            isSit = false;
            isLookUp = true;

            UpperAnim.SetBool("LookUP", isLookUp);
        }
        //가만히 있을 경우
        else
        {
            isLookUp = false;
            isLookDown = false;
            isSit = false;

            UpperAnim.SetBool("LookUP", isLookUp);
            UpperAnim.SetBool("LookDown", isLookDown);
            CharacterChange(isSit);
        }



        if (isSit)
        {
            speed = 1f;
            UpperCollider.enabled = false;
            AllBodyCollider.enabled = true;
            CharacterChange(isSit);
            if(isGetHw) AllBodyAnim.SetBool("IsGetHw", isGetHw);
        }
        else
        {
            speed = 3f;
            AllBodyCollider.enabled = false;
            UpperCollider.enabled = true;
            CharacterChange(isSit);
        }



        //총쏘기
        if (canShoot && Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
            StartCoroutine(ShootCooldown());
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isJump)
        {
            Jump();
        }

        transform.position += new Vector3(movement.x, 0, 0) * Time.deltaTime * speed;


        AllBodyAnim.SetFloat("PositionX", movement.x);
        AllBodyAnim.SetFloat("PositionY", movement.y);
        UpperAnim.SetFloat("PositionX", movement.x);
        UpperAnim.SetFloat("PositionY", movement.y);
        LowerAnim.SetFloat("PositionX", movement.x);
        LowerAnim.SetFloat("PositionY", movement.y);
    }



    void Fire()
    {
        if (isLookUp) UpperAnim.SetTrigger("LookUpFire");
        else if (isLookDown) UpperAnim.SetTrigger("LookDownFire");
        else
        {
            UpperAnim.SetTrigger("Fire");
            AllBodyAnim.SetTrigger("Fire");
        }


        if (isMelee)
        {
            RandomNum = Random.Range(0, 1f);
            UpperAnim.SetFloat("MeleeNum", RandomNum);
            AllBodyAnim.SetFloat("MeleeNum", RandomNum);
        }
        else
        {
            StartCoroutine(FireBullet());
        }
    }

    private IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(weapon.SHOOTINTERVAL);
        canShoot = true;
    }

    IEnumerator FireBullet()
       {
        while(true)
        {
            for (int i = 0; i < weapon.FIRESPEED; i++)
            {
                yield return new WaitForSeconds(0.15f);
                var bullet = ObjectPool.instance.GetBullet(WeaponName);
                if (isSit)
                {
                    bullet.transform.position = firePosObj[1].transform.position;
                    bullet.transform.rotation = firePosObj[1].transform.rotation;
                }
                else
                {
                    bullet.transform.position = firePosObj[0].transform.position;
                    bullet.transform.rotation = firePosObj[0].transform.rotation;
                }

                bullet.FireBullet(bDir, weapon.SHOTSPEED);
            }

            break;
        }    
    }
    void Jump()
    {
        if (!isJump)
        {
            if (isSit)
            {
                isSit = false;
                CharacterChange(isSit);
            }

            isJump = true;
            rigid.AddForce(Vector2.up * 6f, ForceMode2D.Impulse);

            UpperAnim.SetBool("IsJump", isJump);
            LowerAnim.SetBool("IsJump", isJump);
        }
    }


    private void Update()
    {

        if (!isDie)
        {
            HandleInput();
            UpdateAnimations();
        }
    }

    private void FixedUpdate()
    {
        Vector2 playerPosition = transform.position + raycast;
        RaycastHit2D hit = Physics2D.Raycast(playerPosition, dir, Mathf.Infinity, enemyLayer);

        if (hit.collider != null)
        {
            float distance = hit.distance;
            if (distance < 0.6f) isMelee = true;
            else isMelee = false;
        }

        //Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), dir, new Color(0, 1, 0));

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.layer == LayerMask.NameToLayer("Road"))
        {
           
            isJump = false;
            UpperAnim.SetBool("IsJump", isJump);
            LowerAnim.SetBool("IsJump", isJump);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("EnemyAttack")))
        {
            Die();
        }

        if (collision.CompareTag("Enemy"))
        {

            isMelee = true;

            UpperAnim.SetBool("IsMelee", isMelee);
            AllBodyAnim.SetBool("IsMelee", isMelee);
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            isMelee = true;
            UpperAnim.SetBool("IsMelee", isMelee);
            AllBodyAnim.SetBool("IsMelee", isMelee);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isMelee = false;
        UpperAnim.SetBool("IsMelee", isMelee);
        AllBodyAnim.SetBool("IsMelee", isMelee);
    }


}
