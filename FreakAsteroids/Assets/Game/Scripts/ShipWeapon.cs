using System;
using UnityEngine;

public class ShipWeapon : MonoBehaviour
{
    private ILauncher _launcher;

    [SerializeField] 
    private Transform weaponMountPoint;
    [SerializeField] 
    private float fireRefreshRate = 0.5f;
    private float _nextFireTime;
    private void Awake()
    {
        _launcher = GetComponent<ILauncher>();
        GetComponent<ShipInput>().OnFire += FireWeapon;
    }

    private void FireWeapon()
    {
        if (CanFire())
        {
            _nextFireTime = Time.time + fireRefreshRate;
            _launcher.Launch(this);
        }
    }

    public Transform FirePoint()
    {
        return weaponMountPoint;
    }
    
    private bool CanFire()
    {
        return Time.time >= _nextFireTime;
    }
    
}