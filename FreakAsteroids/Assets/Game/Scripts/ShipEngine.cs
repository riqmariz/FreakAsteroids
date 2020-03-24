using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class ShipEngine : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 100;

    [SerializeField] 
    private float thrustForce = 10f;

    [SerializeField] 
    private float maxSpeed= 7f;
    
    private ShipInput _shipInput;
    private Rigidbody2D _rb;
    
    public void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _shipInput = GetComponent<ShipInput>();
        _shipInput.OnThrust += Thrust;
    }

    private void Thrust()
    {
        _rb.AddForce(thrustForce * Time.fixedDeltaTime * transform.up, ForceMode2D.Impulse);
        _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, maxSpeed);

        //_rb.velocity = new Vector2(Mathf.Clamp(_rb.velocity.x, -maxSpeed, maxSpeed),Mathf.Clamp(_rb.velocity.y, -maxSpeed, maxSpeed));
    }

    public void Update()
    {
        transform.position = GameUtility.CheckPositionAndTeleport(transform.position);
        ControlShipRotation();
        Debug.Log("speed: "+_rb.velocity);
    }

    private void ControlShipRotation()
    {
        transform.Rotate(Time.deltaTime * (-_shipInput.Horizontal) * rotationSpeed * Vector3.forward );
    }
    
}