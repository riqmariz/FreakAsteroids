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

    [SerializeField] 
    private float buffDuration = 5f;
    
    public GameObject Target { get => _target; set => _target = value; }
    public float Duration { get => buffDuration; set => buffDuration = value; }

    private Rigidbody2D _rb;
    private GameObject _target;
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


    public void Apply()
    {
        Timers.CreateClock(Target, Duration, () =>
            {
                Debug.Log("Starting SideBulletsPowerUp");
                var weapon = Target.GetComponent<SideBulletsLauncher>();
                if (weapon == null) Target.AddComponent<SideBulletsLauncher>();
                weapon.enabled = true;
            },
            () => Remove()
        );
    }

    public void Remove()
    {
        Debug.Log("Removing SideBulletsPowerUp");
        var weapon = Target.GetComponent<SideBulletsLauncher>();
        Destroy(weapon);
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _target = other.gameObject;
            Apply();
            Destroy(gameObject);
        }
    }

}