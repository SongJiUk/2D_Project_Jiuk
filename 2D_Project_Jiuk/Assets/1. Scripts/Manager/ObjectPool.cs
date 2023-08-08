using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance = null;

    private void Awake()
    {
        if (null == instance) instance = this;
        else Destroy(this.gameObject);
    }

}
