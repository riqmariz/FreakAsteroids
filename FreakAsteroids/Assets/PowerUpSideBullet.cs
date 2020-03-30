using System;
using UnityEditor;
using UnityEngine;

public class PowerUpSideBullet : MonoBehaviour, IPowerUp
{
    [Header("PowerUp Movement")]
    [SerializeField]
    private float speedForce = 10f;
    [Header("PowerUp Time")] 
    [SerializeField]
    private float timeToSelfDestruct = 4f;
    
    private Rigidbody2D _rb;
    private float _willSelfDestruct;
    public void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        transform.position = GameUtility.CheckPositionAndTeleport(transform.position);
        if (Time.time >= _willSelfDestruct)
        {
            Destroy(gameObject);
        }
    }

    public void Drop(Vector3 direction)
    {
        _rb.AddForce(direction * speedForce, ForceMode2D.Impulse);
        _willSelfDestruct = Time.time + timeToSelfDestruct;
    }

    public void Buff(GameObject ship)
    {
        if (ship.GetComponent<SideBulletsLauncher>() == null)
        {
            ship.AddComponent<SideBulletsLauncher>();
        }
        else
        {
            ship.GetComponent<SideBulletsLauncher>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Buff(other.gameObject);
            Destroy(gameObject);
        }
    }
}