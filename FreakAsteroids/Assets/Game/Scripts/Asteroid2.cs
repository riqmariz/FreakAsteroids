using System;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Asteroid2 : MonoBehaviour, IHealth
{
    [SerializeField]
    private float minSpeed;
    [SerializeField]
    private float maxSpeed;

    [SerializeField] 
    private float maxRotation;

    public int Generation { get; set; }

    private Rigidbody2D _rb;
    private float _rotation;

    protected int health = 1;
    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public event Action<float> OnHPChanged = delegate { };
    public event Action OnDied = delegate { };
    
    private void Awake()
    {
        _rb =GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ApplyForceInARandomDirection();
        _rotation = RandomRotation();
    }

    private float RandomRotation()
    {
        return Random.Range(-maxRotation, maxRotation);
    }

    private void ApplyForceInARandomDirection()
    {
        //later check if changing to float make it better
        int randomDirectionX = Random.Range(0, 2);
        var dirX = randomDirectionX == 1 ?  1 : -1;

        int randomDirectionY = Random.Range(0, 2);
        var dirY = randomDirectionY == 1 ? 1 : -1;
        
        float randomSpeedX = Random.Range(minSpeed, maxSpeed);
        float randomSpeedY = Random.Range(minSpeed, maxSpeed);
        
        Vector2 speed = new Vector2(randomSpeedX,randomSpeedY);
        Vector2 dir = new Vector2(dirX,dirY);
       Debug.Log("speed: "+speed);
        _rb.AddForce(dir * speed, ForceMode2D.Impulse);
    }


    public void Update()
    {
        RotateAsteroid();
    }

    private void RotateAsteroid()
    {
        transform.Rotate(_rotation * Time.deltaTime * transform.forward);
    }

    public void FixedUpdate()
    {
        transform.position = GameUtility.CheckPositionAndTeleport(transform.position);
    }

    public void TakeDamage(int value)
    {
        health -= value;
        OnHPChanged(health);
        if (health <= 0)
        {
            DestroyAsteroid();
        }
    }

    private void DestroyAsteroid()
    {
        OnDied();
        if (Generation < 3)
        {
            CreateSmallAsteriods(2);
        }
        
        //gameController.AsteroidDestroyed(generation);
        Destroy(gameObject, 0.01f);
    }

    void CreateSmallAsteriods(int asteroidsNum)
    {
        int newGeneration = Generation + 1;
        for (int i = 1; i <= asteroidsNum; i++)
        {
            float scaleSize = 0.5f;
            
            GameObject asteroidClone = Instantiate(gameObject, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            //AsteroidClone.transform.parent = gameController.transform;
            var localScale = asteroidClone.transform.localScale;
            localScale = new Vector3(localScale.x * scaleSize, localScale.y * scaleSize, localScale.z * scaleSize);
            asteroidClone.transform.localScale = localScale;
            asteroidClone.GetComponent<Asteroid2>().Generation = newGeneration;
            asteroidClone.SetActive(true);
            
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            other.gameObject.GetComponent<IHealth>().TakeDamage(1);
        }
    }
    
}