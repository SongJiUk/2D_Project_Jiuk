using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creature
{
    public static Player instace = null;

    private void Awake()
    {
        if (null == instace) instace = this;
        else Destroy(this.gameObject);

    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {

        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {

        }
        else if(Input.GetKey(KeyCode.UpArrow))
        {

        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {

        }
    }
}
