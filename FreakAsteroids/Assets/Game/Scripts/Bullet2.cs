using System;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Bullet2 : MonoBehaviour
{
    [SerializeField] 
    private float bulletSpeed = 15f;

    [SerializeField] 
    private float timeToSelfDestruct = 1.5f;
    
    private Rigidbody2D _rb;
    private float _willSelfDestruct;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector3 dir)
    {
        _rb.AddForce(dir * bulletSpeed, ForceMode2D.Impulse);
        _willSelfDestruct = Time.time + timeToSelfDestruct;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            var damageComponent = other.GetComponent<ITakeDamage>();
            damageComponent.TakeDamage(1);
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        transform.position = GameUtility.CheckPositionAndTeleport(transform.position);
        
        if (Time.time >= _willSelfDestruct)
        {
            Destroy(gameObject);
        }
    }
}