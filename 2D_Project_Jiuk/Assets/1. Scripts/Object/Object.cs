using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public int hp;

    [SerializeField] GameObject OffObject;
    [SerializeField] GameObject OnObject;
    [SerializeField] GameObject HitEffect;

    
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
        StartCoroutine(HitAnim());
        hp--;
        if(hp <= 0)
        {
            hp = 0;
            Break();
        }
    }
    
    IEnumerator HitAnim()
    {
        if(HitEffect != null) HitEffect.SetActive(true);
        yield return new WaitForSeconds(0.1f);

        if(HitEffect != null) HitEffect.SetActive(false);
        yield return new WaitForSeconds(0.1f);
    }

    public void Break()
    {
        //터짐 
        if(null != OffObject) OffObject.SetActive(false);
        if (null != OnObject) OnObject.SetActive(true);
    }
}
