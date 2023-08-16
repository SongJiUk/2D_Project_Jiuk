using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creature
{
    public static Player instace = null;
    
    Vector2 movement = Vector2.zero;
    Vector3 left = new Vector3(-1f, 1f, 1f);
    Vector3 right = new Vector3(1f, 1f, 1f);

    float speed = 3f;
    bool isShoot = false;
    bool isJump = false;
    bool isSit = false;
    bool isLookUp = false;
    bool isLookDown = false;
    bool isGetHw = false;
    bool isMelee = false;

    [Header("캐릭터 관련")]
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] GameObject[] CharacterBody;

    [Header("캐릭터 애니메이션 관련")]
    [SerializeField] Animator UpperAnim;
    [SerializeField] Animator LowerAnim;
    [SerializeField] Animator AllBodyAnim;

    [Header("총 발사 관련")]
    [SerializeField] GameObject[] firePosObj;
    [SerializeField]
    GameObject[] BulletObj;
    Vector3 Firepos = Vector3.zero;
    Vector3 UpFirepos = Vector3.zero;
    Vector3 DownFirepos = Vector3.zero;
    float RandomNum;
    private void Awake()
    {
        if (null == instace) instace = this;
        else Destroy(this.gameObject);


        Firepos = firePosObj[0].transform.localPosition;
        UpFirepos = firePosObj[0].transform.localPosition + new Vector3(-1f, 1f, 0);
        DownFirepos = firePosObj[0].transform.localPosition + new Vector3(-0.9f, -1.2f, 0);


        firePosObj[0].transform.localPosition = Firepos;
        isGetHw = true;
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


    void Hit()
    {

    }

    void Die()
    {

    }

    void Attack()
    {
        
    }

    void Jump()
    {
        
        
    }

    void Sit()
    {

        
       
    }

    void Move()
    {
        
    }

    void ChangeWeapon()
    {

    }

    private void Update()
    {   

        /*
         * 
        상, 하체 분리 :
        1. 상체 :
        1.1 - 폭탄 - E키로 따로 만들기 
        1.2 - 총발사 - space로 공격 키 만듬
        1.3 - 점프 - shift로 만들었음( 점프시 isJump = ture)
        1.4 - 앞으로 가면서 점프 (isJump일경우 이동할)
        1.5 - Idle
        1.6 - 이동 - 
        1.7 - 근접공격 - 주변에 적이있으면 총쏘지않고 근접공격
        1.8 - 위에 봄
        1.9 - 위에보고 총쏨 - 위 보고있으면 총쏠때 수정
        1.10 - 점프하면서 아래봄 - isJump, 아래 보면 총쏘딘
        1.11 - 점프하면서 총

        2. 하체 :
        2.1 - Idle
        2.2 - 이동
        2.3 - 점프
        2.4 - 앞으로 점프


        상하체 하나 :
        1. 앉을경우
        1.1 - 폭탄
        1.2 - 총발사
        1.3 - Idle
        1.4 - 근접공격
        1.5 - 이동

        2. 탱크에서 나올경우, 탈경우

        3. 죽을경우 


        */
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        

        if(movement.x != 0)
        {
            //firePosObj.transform.position += new Vector3(movement.x, movement.y, 0) * Time.deltaTime;
        }

        if(!isGetHw)
        {
            //Firepos = firePosObj.transform.position;
            //firePosObj.transform.position = 
        }


        //좌 우 변경
        if (movement.x < 0)
        {
            transform.localScale = left;
        }
        else if (movement.x > 0)
        {
            transform.localScale = right;
        }


        if (movement.y < 0)
        {
            if (isJump)
            {
                isSit = false;
                isLookDown = true;
                firePosObj[0].transform.localPosition = DownFirepos;
            }
            else isSit = true;
        }
        else if (movement.y > 0)
        {
            isLookUp = true;
            firePosObj[0].transform.localPosition = UpFirepos;
            isSit = false;
        }
        else
        {
            isLookUp = false;
            firePosObj[0].transform.localPosition = Firepos;
            isLookDown = false;
            isSit = false;
            CharacterChange(isSit);
        }

        if (isSit)
        {
            speed = 1f;
            CharacterChange(isSit);
        }
        else
        {
            speed = 3f;
            CharacterChange(isSit);
        }

        //총쏘기
        if (Input.GetKeyDown(KeyCode.Space))
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

            }
            //var bullet = ObjectPool.instance.bullet_que.Dequeue();

            //bullet.SetActive(true);
            //bullet.transform.position = firePosObj[0].transform.position;
            //bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 100f);

        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
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

        //이동
        transform.position += new Vector3(movement.x, 0, 0) * Time.deltaTime * speed;


        if (!isJump)
        {
            //CharacterChange(isSit);

        }
        if (isJump)
        {
            /*
             * 어느 상황에서라도 가능한것 - 죽기, 승
             * 
             * 
              점프중일때 가능한것
            1. 총쏘기
            2. 근접공격
            3. 가만히 있기
            4. 수류탄 던지기
            5. 밑으로 총쏘기
            6. 위로 총쏘기
            7. 탈것 타기

             막아야 할것
            1. 앉기
             */

            isSit = false;

            

            

        }
        else
        {
            /*
              점프가 아닐때 가능한것
            1. 가만히 있기
            2. 위에 보기, 총쏘기
            3. 수류탄 던지기
            4. 총쏘기
            5. 이동하기
            6. 앉기 - 이동, 총쏘기
             */

            if(isSit)
            {

            }
            else
            {

            }


        }

        


        //앉았을 경우에 오브젝트 활성화, 비활성


        AllBodyAnim.SetFloat("PositionX", movement.x);
        AllBodyAnim.SetFloat("PositionY", movement.y);
        UpperAnim.SetFloat("PositionX", movement.x);
        UpperAnim.SetFloat("PositionY", movement.y);
        LowerAnim.SetFloat("PositionX", movement.x);
        LowerAnim.SetFloat("PositionY", movement.y);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJump = false;

        UpperAnim.SetBool("IsJump", isJump);
        LowerAnim.SetBool("IsJump", isJump);
        AllBodyAnim.SetFloat("PositionY", 0f);
    }


    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {

            isMelee = true;

            UpperAnim.SetBool("IsMelee", isMelee);

        }

        if(collision.CompareTag("Weapon"))
        {
            Debug.Log(collision.GetComponent<Weapon>().EWEAPON);
            isGetHw = true;
            UpperAnim.SetBool("IsGetHw", isGetHw);
        }
        
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            isMelee = true;
            UpperAnim.SetBool("IsMelee", isMelee);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isMelee = false;
        UpperAnim.SetBool("IsMelee", isMelee);
    }


}
