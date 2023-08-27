using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] BoxCollider2D trigger;
    bool TakeBoat = false;
    Vector3 dir = Vector3.zero;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            TakeBoat = true;
            dir = Vector3.right * 10f;
            trigger.enabled = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy")))
        {
            TakeBoat = true;
            dir = Vector3.zero;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy")))
        {
            TakeBoat = true;
            dir = Vector3.zero;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy")))
        {
            TakeBoat = true;
            dir = Vector3.right * 10f;
        }
    }

    private void Update()
    {
        if(TakeBoat) rigid.AddForce(dir * Time.deltaTime);
    }
}
