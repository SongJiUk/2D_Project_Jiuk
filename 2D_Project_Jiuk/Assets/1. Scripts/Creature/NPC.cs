using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Creature
{
    [SerializeField] int PoseNum;
    [SerializeField] Animator anim;
    [SerializeField] GameObject GiveItemPos;
    bool takeoff;

    private void Start()
    {
        anim.SetInteger("PoseNum", PoseNum);
    }

    public void GiveItem()
    {

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            anim.SetTrigger("Contact");
        }
    }


}
