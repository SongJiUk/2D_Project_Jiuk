using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_Crab_Attack2 : MonoBehaviour
{
    [SerializeField] Animator anim;
    Vector3 dir = Vector3.zero;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.SetTrigger("isHit");
    }

    public void EndAnim()
    {
        Destroy(gameObject);
    }

    public void SetDir(Vector3 _dir)
    {
        dir = _dir;
    }
    private void Update()
    {
        transform.position += dir * Time.deltaTime;
    }
}
