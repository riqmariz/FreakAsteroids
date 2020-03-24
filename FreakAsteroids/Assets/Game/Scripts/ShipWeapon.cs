using System;
using UnityEngine;

public class ShipWeapon : MonoBehaviour
{
    private ILauncher _launcher;

    [SerializeField] 
    private float fireRefreshRate = 0.5f;
    private float _nextFireTime;
    private bool _shoot;
    private void Awake()
    {
        _launcher = GetComponent<ILauncher>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && CanFire())
        {
            _shoot = true;
        }
    }

    private void FixedUpdate()
    {
        if (_shoot)
        {
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        _nextFireTime = Time.time + fireRefreshRate;
        _launcher.Launch(this);
        _shoot = false;
    }

    private bool CanFire()
    {
        return Time.time >= _nextFireTime;
    }
    
}