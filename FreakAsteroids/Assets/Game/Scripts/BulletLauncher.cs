using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class BulletLauncher : Launcher
{
    [SerializeField] protected NormalBullet bulletPrefab;

    private IOnLaunch _onLaunch;

    protected override void Launch(ShipWeapon weapon)
    {
        var bullet = Instantiate(bulletPrefab,weapon.FirePoint().position,transform.rotation);
        bullet.Launch(weapon.transform.up);
    }
}