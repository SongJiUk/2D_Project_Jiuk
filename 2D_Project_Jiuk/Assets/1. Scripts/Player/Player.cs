using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creature
{
    public static Player instace = null;
    Vector2 movement = Vector2.zero;

    Vector3 left = new Vector3(-1f, 1f, 1f);
    Vector3 right = new Vector3(1f, 1f, 1f);

    [SerializeField] Animator UpperAnim;
    [SerializeField] Animator LowerAnim;

    private void Awake()
    {
        if (null == instace) instace = this;
        else Destroy(this.gameObject);

    }

    private void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (movement.x < 0) transform.localScale = left;
        else if (movement.x > 0) transform.localScale = right;

        UpperAnim.SetFloat("PositionX", movement.x);
        UpperAnim.SetFloat("PositionY", movement.y);
        LowerAnim.SetFloat("PositionX", movement.x);
        LowerAnim.SetFloat("PositionY", movement.y);


        if(Input.GetKeyDown(KeyCode.Space))
        {
            UpperAnim.SetTrigger("Fire");
        }
    }
}
