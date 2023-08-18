using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance = null;

    const int BulletCount = 40;
    const int NumberOfWeapon = 10;

    [Header("총알 생성 관련 ")]
    [SerializeField] string[] bullet_name;
    [SerializeField] GameObject[] Bullets;
    [SerializeField] GameObject[] Bullets_parent;
    public Queue<GameObject>[] bullet_que;
    public Dictionary<string, Queue<GameObject>> Bullet_Dic = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        if (null == instance) instance = this;
        else Destroy(this.gameObject);

        
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        CreateBullet();
    }


    public void CreateBullet()
    {
        bullet_que = new Queue<GameObject>[NumberOfWeapon];
        for (int j = 0; j < NumberOfWeapon; j++)
        {
            bullet_que[j] = new Queue<GameObject>();

            for (int i = 0; i < BulletCount; i++)
            {
                var bullet = Instantiate(Bullets[j], Bullets_parent[j].transform);
                bullet.SetActive(false);
                bullet_que[j].Enqueue(bullet);
            }

            Bullet_Dic.Add(bullet_name[j], bullet_que[j]);
        }
    }

    public GameObject GetBullet(string key)
    {

        if (Bullet_Dic[key].Count > 0)
        {
            GameObject bullet = Bullet_Dic[key].Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else return null;
    }

    public void ReturnBullet(GameObject bullet, string key)
    {
        bullet.SetActive(false);
        Bullet_Dic[key].Enqueue(bullet);
    }
}











