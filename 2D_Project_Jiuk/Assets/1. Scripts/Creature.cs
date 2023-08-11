using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{

    private float hp;
    private float damage;

    public float HP
    {
        get;
        set;
    }

    public float DAMAGE
    {
        get;
        set;
    }



    protected void Init()
    {

    }

    protected void Hit()
    {
        HP--;
        if (HP <= 0)
        {
            HP = 0;
            Die();
        }
    }

    protected void Die()
    {

    }

    protected void Attack()
    {

    }

    protected void Jump()
    {

    }

    protected void Sit()
    {

    }

    protected void Move()
    {

    }

}
