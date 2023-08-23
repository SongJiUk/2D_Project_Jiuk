using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Weapon
{

    void Start()
    {
        if(eWeapon == EWeaponName.Default)
        {
            GetWeapon(eWeapon);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy")))
        {
            ObjectPool.instance.ReturnBullet(this, eWeapon.ToString());
        }
    }

    private void Update()
    {
        
    }
}
