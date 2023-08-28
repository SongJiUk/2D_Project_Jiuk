using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance = null;

    const int BulletCount = 50;
    const int NumberOfWeapon = 10;
    const int GrenadeCount = 10;

    [Header("총알 생성 관련 ")]
    [SerializeField] string[] bullet_name;
    [SerializeField] GameObject[] Bullets;
    [SerializeField] GameObject[] Bullets_parent;
    public Queue<Bullet>[] bullet_que;
    public Dictionary<string, Queue<Bullet>> Bullet_Dic = new Dictionary<string, Queue<Bullet>>();

    [Header("폭탄 관련")]
    [SerializeField] GameObject Grenade;
    public Queue<Grenade> Grenades_que = new Queue<Grenade>();
    private void Awake()
    {
        if (null == instance) instance = this;
        else Destroy(this.gameObject);

        
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        CreateBullet();
        CreateGrenade();
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
                bullet.SetActive(false);
                bullet_que[j].Enqueue(bullet.GetComponent<Bullet>());
            }

            Bullet_Dic.Add(bullet_name[j], bullet_que[j]);
        }
    }

    public void CreateGrenade()
    {
        for(int i=0; i< GrenadeCount; i++)
        {
            var grenade = Instantiate(Grenade, transform);
            grenade.SetActive(false);
            Grenades_que.Enqueue(grenade.GetComponent<Grenade>());
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
        else
        {
            int num =  (int)findname(key);
            var bullet = Instantiate(Bullets[num], Bullets_parent[num].transform);
            bullet.SetActive(false);
            Bullet_Dic[key].Enqueue(bullet.GetComponent<Bullet>());

            Bullet bullets = Bullet_Dic[key].Dequeue();
            bullets.gameObject.SetActive(true);
            return bullets;

        }
    }

    public EWeaponName findname(string name)
    {
        switch (name)
        {
            case "Default":
                return EWeaponName.Default;
            case "Heavy_Machinegun":
                return EWeaponName.Heavy_Machinegun;
            case "Flame_Shot":
                return EWeaponName.Flame_Shot;
            case "Shot_Gun":
                return EWeaponName.Shot_Gun;
            case "Super_Grenade":
                return EWeaponName.Super_Grenade;
            case "Rocket_Launcher":
                return EWeaponName.Rocket_Launcher;
            case "Laser_Gun":
                return EWeaponName.Laser_Gun;
            case "Iron_Lizard":
                return EWeaponName.Iron_Lizard;
            case "Drop_Shot":
                return EWeaponName.Drop_Shot;
        }

        return 0;
    }

    public void ReturnBullet(Bullet bullet, string key)
    {
        bullet.gameObject.SetActive(false);
        Bullet_Dic[key].Enqueue(bullet);
    }

    public Grenade GetGrenade()
    {
        Grenade grenade = Grenades_que.Dequeue();
        grenade.gameObject.SetActive(true);
        return grenade;
    }

    public void ReturnGrenade(Grenade grenade)
    {
        grenade.gameObject.SetActive(false);
        Grenades_que.Enqueue(grenade);
    }
}











