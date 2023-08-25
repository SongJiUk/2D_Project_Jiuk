using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Weapon
{
    Vector3 Dir = Vector3.zero;
    

    public GameObject explosionPrefab;
    public GameObject NormalEffectPrefab;
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
            if(ISEXPLOSIONBULLET)
            {
                GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                ObjectPool.instance.ReturnBullet(this, eWeapon.ToString());

                Destroy(explosion, 0.4f);
                
            }
            else
            {
                GameObject explosion = Instantiate(NormalEffectPrefab, transform.position, Quaternion.identity);
                ObjectPool.instance.ReturnBullet(this, eWeapon.ToString());
                Destroy(explosion, 0.4f);
            }
        }

        if(collision.gameObject.layer.Equals(LayerMask.NameToLayer("NPC")))
        {
            ObjectPool.instance.ReturnBullet(this, eWeapon.ToString());
        }
    }

    public void FireBullet(Vector3 _dir, float _shootSpeed)
    {
        Dir = _dir;
        SHOTSPEED = _shootSpeed;
        StartCoroutine(FireBullet());
    }

    IEnumerator FireBullet()
    {
        while(true)
        {
            yield return null;
            transform.position += transform.right * Time.deltaTime * SHOTSPEED;


        }
    }

    private void OnDisable()
    {
        StopCoroutine(FireBullet());
    }


    bool IsVisible()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        // 화면 밖으로 나간 경우를 판단
        return (viewportPosition.x > 0 && viewportPosition.x < 1 && viewportPosition.y > 0 && viewportPosition.y < 1);

    }

    private void Update()
    {
        if(!IsVisible())
        {
            ObjectPool.instance.ReturnBullet(this, eWeapon.ToString());
        }
    }

}
