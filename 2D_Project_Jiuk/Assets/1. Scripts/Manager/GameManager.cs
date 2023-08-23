using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private void Awake()
    {
        if (null == instance) instance = this;
        else Destroy(this.gameObject);

        DontDestroyOnLoad(this);
    }



}
