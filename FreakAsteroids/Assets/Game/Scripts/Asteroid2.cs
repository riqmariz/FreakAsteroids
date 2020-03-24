using UnityEngine;

public class Asteroid2 : MonoBehaviour, ITakeDamage
{
    private int _generation;
    public int Generation
    {
        get { return _generation; }
        set { _generation = value; }
    }
    
    private MovementComponent _movementComponent;
    private Rigidbody2D _rb;
    private float rotation;
    private float impulseForce;

    private void Awake()
    {
        _movementComponent = GetComponent<MovementComponent>();
    }

    public void Update()
    {
        MoveAndRotate();
    }

    private void MoveAndRotate()
    {
        /*
        asteroid.transform.Rotate(new Vector3(0, 0, rotationZ) * Time.deltaTime);
        _movementComponent.Move(finalSpeedX*Time.deltaTime,finalSpeedY*Time.deltaTime);
        */
        transform.Rotate(rotation * Time.deltaTime * transform.forward);
        _rb.AddForce(impulseForce * transform.up,ForceMode2D.Impulse);
    }

    public void FixedUpdate()
    {
        transform.position = GameUtility.CheckPositionAndTeleport(transform.position);
    }

    public void TakeDamage(int value)
    {
        if (_generation < 3)
        {
            CreateSmallAsteriods(2);
        }

        DestroyAsteroid();
    }

    private void DestroyAsteroid()
    {
        //gameController.AsteroidDestroyed(generation);
        Destroy(gameObject, 0.01f);
    }

    void CreateSmallAsteriods(int asteroidsNum)
    {
        int newGeneration = _generation + 1;
        for (int i = 1; i <= asteroidsNum; i++)
        {
            float scaleSize = 0.5f;
            /*
            GameObject AsteroidClone = Instantiate(asteroid, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            AsteroidClone.transform.parent = gameController.transform;
            AsteroidClone.transform.localScale = new Vector3(AsteroidClone.transform.localScale.x * scaleSize, AsteroidClone.transform.localScale.y * scaleSize, AsteroidClone.transform.localScale.z * scaleSize);
            AsteroidClone.GetComponent<Asteroid2>().Generation = newGeneration;
            AsteroidClone.SetActive(true);
            */
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