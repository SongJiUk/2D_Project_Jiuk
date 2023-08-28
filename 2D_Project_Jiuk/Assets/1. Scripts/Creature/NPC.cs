using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Creature
{
    [SerializeField] int PoseNum;
    [SerializeField] Animator anim;
    [SerializeField] GameObject GiveItemPos;

    [SerializeField] GameObject[] Itmes;
    public bool isGood;
    bool takeoff;
    bool isWalk = false;
    bool isRun = false;

    public float moveSpeed = 2.0f;
    public float moveRange = 5.0f;

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    Vector3 right = Vector3.zero;
    Vector3 left = new Vector3(0f, 180f, 0f);
    private bool movingRight = true;

    private void Start()
    {
        anim.SetInteger("PoseNum", PoseNum);

       
    }

    private void Update()
    {
        if (isWalk)
        {
            if (movingRight)
            {
                transform.rotation = Quaternion.Euler(right);
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;

                if (Vector3.Distance(transform.position, originalPosition) <= 0.1f)
                {
                    
                    movingRight = false;
                }
            }
            else
            {
                transform.rotation = Quaternion.Euler(left);
                transform.position -= Vector3.right * moveSpeed * Time.deltaTime;

                if (Vector3.Distance(transform.position, targetPosition) >= moveRange)
                {
                    movingRight = true;
                }
            }
        }

        if (isRun)
        {
            moveSpeed = 5f;

            if (movingRight) transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            else transform.position -= Vector3.right * moveSpeed * Time.deltaTime;
        }
    }
    public void Walk()
    {
        isWalk = true;
        originalPosition = transform.position;
        targetPosition = originalPosition + Vector3.right * moveRange;
    }


    public void GiveItem()
    {
        if (isGood)
        {
            var a = Instantiate(Itmes[1]);
            a.SetActive(true);
            a.transform.position = GiveItemPos.transform.position;
        }
        else
        {
            var a =Instantiate(Itmes[0]);
            a.SetActive(true);
            a.transform.position = GiveItemPos.transform.position;
        }
    }

    public void Run()
    {
        isRun = true;
        Invoke("DestroyNPC", 2f);
    }

    public void DestroyNPC()
    {
        Destroy(gameObject);
    }

    public void ChangeLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("TakeOffNPC");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("PlayerAttack")))
        {
            anim.SetTrigger("TakeOff");
        }

        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("GrenadeAttack")))
        {
            anim.SetTrigger("TakeOff");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            anim.SetTrigger("Contact");
            isWalk = false;
        }
    }


}
