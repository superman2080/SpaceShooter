using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : GunCtrl
{
    [Range(1, 10)]
    public float bulletSpread;
    public override void InitialWeapon()
    {
        base.InitialWeapon();
    }


    public override void WeaponBehavior()
    {
        base.WeaponBehavior();
        if (player == null)
            return;
        for (int i = 0; i < 10; i++)
        {
            Quaternion randRot = Quaternion.Euler(new Vector3(Random.Range(-bulletSpread, bulletSpread), 0, Random.Range(-bulletSpread, bulletSpread)));
            BulletCtrl b = Instantiate(player.bulletPrefab, firePos.position, firePos.rotation * randRot).GetComponent<BulletCtrl>();
            b.player = player;
        }
        //StartCoroutine(player.ShowMuzzleFlash(player.muzzleTime));
    }
}
