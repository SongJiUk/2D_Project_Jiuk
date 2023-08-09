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

    [Header("캐릭터 관련")]
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] GameObject[] CharacterBody;

    [Header("캐릭터 애니메이션 관련")]
    [SerializeField] Animator UpperAnim;
    [SerializeField] Animator LowerAnim;
    [SerializeField] Animator AllBodyAnim;
    



    private void Awake()
    {
        if (null == instace) instace = this;
        else Destroy(this.gameObject);

    }

    void Hit()
    {

    }

    void Die()
    {

    }

    void Attack()
    {
        UpperAnim.SetTrigger("Fire");
        AllBodyAnim.SetTrigger("Fire");
        isShoot = true;
    }

    void Jump()
    {
        if (!isJump)
        {
            if(isSit)
            {
                CharacterBody[0].SetActive(true);
                CharacterBody[1].SetActive(true);
                CharacterBody[2].SetActive(false);
                isSit = false;
            }

            isJump = true;
            rigid.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);

            UpperAnim.SetTrigger("IsJump");
            LowerAnim.SetTrigger("IsJump");
        }
        
    }

    void Sit()
    {

        if(!isJump)
        {
            if (isSit)
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
       
    }

    void Move()
    {
        if (isSit) speed = 1f;
        else speed = 3f;

        transform.position += new Vector3(movement.x, 0, 0) * Time.deltaTime * speed;
    }

    private void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (movement.x < 0) transform.localScale = left;
        else if (movement.x > 0) transform.localScale = right;

        if (movement.y < 0)
        {
            movement.y = 0.1f;
            isSit = true;
        }
        else isSit = false;

        

        
        if(isSit)
        {
            AllBodyAnim.SetFloat("PositionX", movement.x);
            AllBodyAnim.SetFloat("PositionY", movement.y);
        }
        else
        {
            UpperAnim.SetFloat("PositionX", movement.x);
            UpperAnim.SetFloat("PositionY", movement.y);
            LowerAnim.SetFloat("PositionX", movement.x);
            LowerAnim.SetFloat("PositionY", movement.y);
        }
       

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Jump();
        }

        Move();
        Sit();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJump = false;
    }
}
