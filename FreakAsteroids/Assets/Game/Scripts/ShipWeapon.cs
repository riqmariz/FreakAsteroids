using System;
using UnityEngine;

public class ShipWeapon : MonoBehaviour, IOnLaunch
{
    [SerializeField] 
    private Transform weaponMountPoint;
    [SerializeField] 
    private float fireRefreshRate = 0.5f;
    private float _nextFireTime;
    public event Action<ShipWeapon> OnLaunch = delegate(ShipWeapon weapon) {};
    private void Awake()
    {
        GetComponent<ShipInput>().OnFire += FireWeapon;
    }

    private void FireWeapon()
    {
        if (CanFire())
        {
            _nextFireTime = Time.time + fireRefreshRate;
            OnLaunch(this);
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