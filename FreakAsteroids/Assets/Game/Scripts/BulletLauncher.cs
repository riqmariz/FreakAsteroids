using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class BulletLauncher : MonoBehaviour,ILauncher
{
    [SerializeField]
    private Bullet2 bulletPrefab;
    
    public void Launch(ShipWeapon weapon)
    {
        var bullet = Instantiate(bulletPrefab,weapon.FirePoint().position,transform.rotation);
        bullet.Launch(weapon.transform.up);
    }
}