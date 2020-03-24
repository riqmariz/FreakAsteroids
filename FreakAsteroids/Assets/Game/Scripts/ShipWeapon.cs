using System;
using UnityEngine;

public class ShipWeapon : MonoBehaviour
{
    private ILauncher _launcher;

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

    private bool CanFire()
    {
        return Time.time >= _nextFireTime;
    }
    
}