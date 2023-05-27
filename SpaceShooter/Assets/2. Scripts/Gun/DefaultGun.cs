using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultGun : GunCtrl
{
    public override void InitialWeapon()
    {
        base.InitialWeapon();
    }

    public override void WeaponBehavior()
    {
        base.WeaponBehavior();
        if(player != null)
        {
            BulletCtrl b = Instantiate(player.bulletPrefab, firePos.position, firePos.rotation).GetComponent<BulletCtrl>();
            b.player = player;
        }
        //StartCoroutine(ShowMuzzleFlash(muzzleTime));
    }
}
