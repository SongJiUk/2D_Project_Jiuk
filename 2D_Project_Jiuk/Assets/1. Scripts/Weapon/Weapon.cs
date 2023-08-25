using UnityEngine;

public class Weapon : MonoBehaviour
{

    public EWeaponName eWeapon;
    

    private bool isExplosionBullet = false;
    public bool ISEXPLOSIONBULLET
    {
        get { return isExplosionBullet; }
        set { isExplosionBullet = value; }
    }

    private float ShotSpeed = 11f;
    public float SHOTSPEED
    {
        get { return ShotSpeed; }
        set { ShotSpeed = value; }
    }

    private float shootInterval = 0.1f;
    public float SHOOTINTERVAL
    {
        get { return shootInterval; }
    }

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

    private bool isHeavyMachinGun;
    public bool ISHEAVYMACHINGUN
    {
        get { return isHeavyMachinGun; }
        set { isHeavyMachinGun = value; }
    }
    public void DefaultWeapon()
    {
        GetWeapon(EWeaponName.Default);
    }

    public void GetWeapon(EWeaponName weaponName)
    {
        switch(weaponName)
        {
            case EWeaponName.Default:
                damage = 1f;
                firespeed = 1f;
                gunmagazine = 999f;
                ShotSpeed = 11f;
                shootInterval = 0.1f;
                isExplosionBullet = false;
                isHeavyMachinGun = false;
                break;

            case EWeaponName.Heavy_Machinegun:
                damage = 1f;
                firespeed = 3f;
                gunmagazine = 200f;
                ShotSpeed = 12f;
                shootInterval = 0.2f;
                isExplosionBullet = false;
                isHeavyMachinGun = true;
                break;

            case EWeaponName.Rocket_Launcher:
                damage = 1f;
                firespeed = 5f;
                gunmagazine = 200f;
                ShotSpeed = 7;
                shootInterval = 1f;
                isExplosionBullet = true;
                isHeavyMachinGun = false;
                break;

            case EWeaponName.Flame_Shot:
                damage = 1f;
                firespeed = 5f;
                gunmagazine = 200f;
                ShotSpeed = 0f;
                shootInterval = 1f;
                isExplosionBullet = false;
                isHeavyMachinGun = false;

                break;

            case EWeaponName.Shot_Gun:
                damage = 20f;
                firespeed = 1f;
                gunmagazine = 30f;
                ShotSpeed = 0f;
                shootInterval = 1f;
                isExplosionBullet = true;
                isHeavyMachinGun = false;
                break;

            case EWeaponName.Drop_Shot:
                damage = 3f;
                firespeed = 1f;
                gunmagazine = 30f;
                ShotSpeed = 3f;
                shootInterval = 1f;
                isExplosionBullet = false;
                isHeavyMachinGun = false;
                break;

            case EWeaponName.Super_Grenade:
                damage = 10f;
                firespeed = 1f;
                gunmagazine = 20f;
                ShotSpeed = 15f;
                shootInterval = 1f;
                isExplosionBullet = true;
                isHeavyMachinGun = false;
                break;

            case EWeaponName.Laser_Gun:
                damage = 1.4f; //발당
                firespeed = 4f;
                gunmagazine = 200f;
                shootInterval = 1f;
                isExplosionBullet = false;
                isHeavyMachinGun = false;
                break;

            case EWeaponName.Enemy_Chaser:
                damage = 1f;
                firespeed = 1f;
                gunmagazine = 40f;
                shootInterval = 1f;
                isExplosionBullet = true;
                isHeavyMachinGun = false;
                break;

            case EWeaponName.Iron_Lizard:
                damage = 3f;
                firespeed = 1f;
                gunmagazine = 30;
                shootInterval = 1f;
                isExplosionBullet = true;
                isHeavyMachinGun = false;
                break;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetWeapon(eWeapon);
        Player.instace.ChangeWeapon(this);

        Destroy(gameObject);
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
