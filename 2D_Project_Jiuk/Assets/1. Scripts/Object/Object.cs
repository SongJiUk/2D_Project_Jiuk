using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public int hp;

    [SerializeField] GameObject OffObject;
    [SerializeField] GameObject OnObject;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("PlayerAttack")))
        {
            Hit();
        }

        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("GrenadeAttack")))
        {
            Hit();
        }
    }

    public void Hit()
    {
        hp--;
        if(hp <= 0)
        {
            hp = 0;
            Break();
        }
    }

    public void Break()
    {
        //터짐 
    }
}
