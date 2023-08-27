using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_Boat : Enemy
{
    bool isBreak;
    void Start()
    {
        SetMonster(25f, 0f, 0f, 0f, 2f);

    }

    // Update is called once per frame
    void Update()
    {
        if(!isBreak) transform.position += Vector3.left * Time.deltaTime * 0.5f;

    }

    private float pushForce = 0.2f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Road")))
        {
            Rigidbody2D otherRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (otherRigidbody != null)
            {
                Vector3 pushDirection = transform.position - collision.transform.position;
                pushDirection.Normalize();
                otherRigidbody.AddForce(-pushDirection * pushForce, ForceMode2D.Impulse);
            }
        }

        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("PlayerAttack")))
        {

            Hit(Player.instace.weapon);
        }

        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("GrenadeAttack")))
        {
            Hit(Player.instace.weapon);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Road")))
        {
            Rigidbody2D otherRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (otherRigidbody != null)
            {
                Vector3 pushDirection = transform.position - collision.transform.position;
                pushDirection.Normalize();
                otherRigidbody.AddForce(-pushDirection * pushForce, ForceMode2D.Impulse);
            }
        }
    }

    protected void Hit(Weapon _weapon)
    {
        //StartHitShineParallel();
        if (HP <= 0)
        {
            HP = 0;
            Die();
        }
    }

}
