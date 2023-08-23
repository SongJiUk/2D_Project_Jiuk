using UnityEngine;

public class Weapon : MonoBehaviour
{

    public EWeaponName eWeapon;


    private float damage;
    public float DAMAGE
    {
        get;
        set;
    }

    //연사 속도
    private float firespeed;
    public float FIRESPEED
    {
        get;
        set;
    }

    //한탄창 갯수 (무한 - 999)
    private float gunmagazine;
    private float GUNMAGAZINE
    {
        get;
        set;
    }

   
    public string WEAPON_NAME
    {
        get;
    }

    private void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetWeapon(eWeapon);
        Player.instace.ChangeWeapon(this);
    }

    public void GetWeapon(EWeaponName weaponName)
    {
        switch(weaponName)
        {
            case EWeaponName.Default:
                DAMAGE = 1f;
                FIRESPEED = 1f;
                GUNMAGAZINE = 999f;
                break;

            case EWeaponName.Heavy_Machinegun:
                DAMAGE = 1f;
                FIRESPEED = 5f;
                GUNMAGAZINE = 200f;
                break;

            case EWeaponName.Rocket_Launcher:
                DAMAGE = 1f;
                FIRESPEED = 5f;
                GUNMAGAZINE = 200f;
                break;

            case EWeaponName.Flame_Shot:
                DAMAGE = 1f;
                FIRESPEED = 5f;
                GUNMAGAZINE = 200f;
                break;

            case EWeaponName.Shot_Gun:
                DAMAGE = 20f;
                FIRESPEED = 1f;
                GUNMAGAZINE = 30f;
                break;

            case EWeaponName.Drop_Shot:
                DAMAGE = 3f;
                FIRESPEED = 1f;
                GUNMAGAZINE = 30f;
                break;

            case EWeaponName.Super_Grenade:
                DAMAGE = 10f;
                FIRESPEED = 1f;
                GUNMAGAZINE = 20f;
                break;

            case EWeaponName.Laser_Gun:
                DAMAGE = 1.4f; //발당
                FIRESPEED = 4f;
                GUNMAGAZINE = 200f;
                break;

            case EWeaponName.Enemy_Chaser:
                DAMAGE = 1f;
                FIRESPEED = 1f;
                GUNMAGAZINE = 40f;
                break;

            case EWeaponName.Iron_Lizard:
                DAMAGE = 3f;
                FIRESPEED = 1f;
                GUNMAGAZINE = 30;
                break;
        }
    }



    /*
     * 데미지, 발사수, 총알 갯수
     * 기본 무기 : 1
     * 헤비머신건 : 1, 한번 누르면 4발, 기본 200
     * R : 3, 1, 30
     * C : 3, 1, 40
     * I : 3, 1, 30
     * D : 3, 1, 30
     * G : 10, 1 ,20
     * S ; 20, 1, 30
     * F, 31, 1, 30
     * 
     * 
     * 수류탄 : 10, 10
     * 혈사포 : 360
     * 
     * 탱크 : 1, 5발
     * 탱크폭탄 : 20
     * 탱크 근접 : 생물일 경우 즉
     */
}
