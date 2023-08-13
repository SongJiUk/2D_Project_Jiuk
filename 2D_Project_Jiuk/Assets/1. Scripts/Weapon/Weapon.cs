using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static Weapon instance = null;

    private void Awake()
    {
        if (null == instance) instance = this;
        else Destroy(this.gameObject);
    }


    private float damage = 1f;
    public float DAMAGE
    {
        get;
        set;
    }

    //연사 속도
    private float autospeed;
    public float AUTOSPEED
    {
        get;
        set;
    }

    //한탄창 갯수
    private float gunmagazine;
    private float GUNMAGAZINE
    {
        get;
        set;
    }


    public void GetWeapon(EWeaponName weaponName)
    {
        switch(weaponName)
        {
            case EWeaponName.Default:
                break;

            case EWeaponName.Heavy_Machinegun:
                break;

            case EWeaponName.Rocket_Launcher:
                break;

            case EWeaponName.Flame_Shot:
                break;

            case EWeaponName.Shot_Gun:
                break;

            case EWeaponName.Drop_Shot:
                break;

            case EWeaponName.Super_Grenade:
                break;

            case EWeaponName.Laser_Gun:
                break;

            case EWeaponName.Enemy_Chaser:
                break;

            case EWeaponName.Iron_Lizard:
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
