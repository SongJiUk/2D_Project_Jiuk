using UnityEngine;

public class Weapon : MonoBehaviour
{

    public EWeaponName eWeapon;

    public float ShotSpeed = 11f;
    private float damage;
    public float DAMAGE
    {
        get { return damage; }
    }

    //연사 속도
    private float firespeed;
    public float FIRESPEED
    {
        get { return firespeed; }
    }

    //한탄창 갯수 (무한 - 999)
    private float gunmagazine;
    public float GUNMAGAZINE
    {
        get { return gunmagazine; }
    }

    public void DefaultWeapon()
    {
        GetWeapon(EWeaponName.Default);
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
                damage = 1f;
                firespeed = 1f;
                gunmagazine = 999f;
                break;

            case EWeaponName.Heavy_Machinegun:
                damage = 1f;
                firespeed = 5f;
                gunmagazine = 200f;
                break;

            case EWeaponName.Rocket_Launcher:
                damage = 1f;
                firespeed = 5f;
                gunmagazine = 200f;
                break;

            case EWeaponName.Flame_Shot:
                damage = 1f;
                firespeed = 5f;
                gunmagazine = 200f;
                break;

            case EWeaponName.Shot_Gun:
                damage = 20f;
                firespeed = 1f;
                gunmagazine = 30f;
                break;

            case EWeaponName.Drop_Shot:
                damage = 3f;
                firespeed = 1f;
                gunmagazine = 30f;
                break;

            case EWeaponName.Super_Grenade:
                damage = 10f;
                firespeed = 1f;
                gunmagazine = 20f;
                break;

            case EWeaponName.Laser_Gun:
                damage = 1.4f; //발당
                firespeed = 4f;
                gunmagazine = 200f;
                break;

            case EWeaponName.Enemy_Chaser:
                damage = 1f;
                firespeed = 1f;
                gunmagazine = 40f;
                break;

            case EWeaponName.Iron_Lizard:
                damage = 3f;
                firespeed = 1f;
                gunmagazine = 30;
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
