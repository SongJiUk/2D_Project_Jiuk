using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance = null;
    
    [SerializeField] GameObject[] Bullets;
    [SerializeField] GameObject[] Bullets_parent;
    public Queue<GameObject> bullet_que = new Queue<GameObject>();
    
    private void Awake()
    {
        if (null == instance) instance = this;
        else Destroy(this.gameObject);

        DontDestroyOnLoad(this);
    }

    private void Start()
    {

        for(int j = 0; j<10;j++)
        {
            for (int i = 0; i < 200; i++)
            {
                var bullet = Instantiate(Bullets[j], Bullets_parent[j].transform);
                bullet.SetActive(false);

                if(j == 0) bullet_que.Enqueue(bullet);
            }
        }
        
    }



}
