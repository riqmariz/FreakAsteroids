using System;
using UnityEngine;

public class ShipInput : MonoBehaviour
{
    public float Horizontal { get; private set; }
    public bool Vertical { get; private set; }
    public bool FireWeapon { get; private set; }

    public event Action OnFire = delegate { };
    
    public event Action OnThrust = delegate { };

    public void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");

        if (Input.GetButton("Vertical"))
        {
            Vertical = true;
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            FireWeapon = true;
        }
        
    }

    public void FixedUpdate()
    {
        if (Vertical)
        {
            OnThrust();
            Vertical = false;
        }

        if (FireWeapon)
        {
            OnFire();
            FireWeapon = false;
        }
    }
}