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
    [SerializeField] Bullet[] Bullets;
    [SerializeField] GameObject[] Bullets_parent;
    public Queue<Bullet>[] bullet_que;
    public Dictionary<string, Queue<Bullet>> Bullet_Dic = new Dictionary<string, Queue<Bullet>>();

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
        bullet_que = new Queue<Bullet>[NumberOfWeapon];
        for (int j = 0; j < NumberOfWeapon; j++)
        {
            bullet_que[j] = new Queue<Bullet>();

            for (int i = 0; i < BulletCount; i++)
            {
                var bullet = Instantiate(Bullets[j], Bullets_parent[j].transform);
                bullet.gameObject.SetActive(false);
                bullet_que[j].Enqueue(bullet);
            }

            Bullet_Dic.Add(bullet_name[j], bullet_que[j]);
        }
    }

    public Bullet GetBullet(string key)
    {

        if (Bullet_Dic[key].Count > 0)
        {
            Bullet bullet = Bullet_Dic[key].Dequeue();
            bullet.gameObject.SetActive(true);
            return bullet;
        }
        else return null;
    }

    public void ReturnBullet(Bullet bullet, string key)
    {
        bullet.gameObject.SetActive(false);
        Bullet_Dic[key].Enqueue(bullet);
    }
}











