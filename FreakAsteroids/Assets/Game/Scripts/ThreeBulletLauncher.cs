﻿using UnityEngine;

public class ThreeBulletLauncher : BulletLauncher
{
    [SerializeField] private float angleBetweenBullets = 45f;
    public override void Launch(ShipWeapon weapon)
    {
        base.Launch(weapon);
        var rotation = transform.rotation;
        var weaponDirection = weapon.transform.up;

        var leftBulletDirection =  Quaternion.Euler(0,0,angleBetweenBullets) * weaponDirection;
        var rotationLeft = Quaternion.Euler(0,0,angleBetweenBullets) * rotation;
        var bulletLeft = Instantiate(bulletPrefab, weapon.FirePoint().position, rotationLeft);
        bulletLeft.Launch(leftBulletDirection);

        var rotationRight = Quaternion.Euler(0,0,-angleBetweenBullets) * rotation;
        var bulletRight = Instantiate(bulletPrefab, weapon.FirePoint().position, rotationRight);
        var rightBulletDirection =  Quaternion.Euler(0,0,-angleBetweenBullets) * weaponDirection;
        bulletRight.Launch(rightBulletDirection);
    }
}