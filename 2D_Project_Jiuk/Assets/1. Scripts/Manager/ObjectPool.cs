using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditorInternal.ReorderableList;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance = null;

    const int BulletCount = 40;
    const int NumberOfWeapon = 10;

    [Header("총알 생성 관련 ")]
    [SerializeField] string[] bullet_name;
    [SerializeField] GameObject[] Bullets;
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
                bullet.SetActive(false);
                bullet_que[j].Enqueue(bullet.GetComponent<Bullet>());
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
}











