using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidController : MonoBehaviour
{
    public GameObject asteroid;
    private GameController gameController;
    public float maxRotation;
    public float maxSpeed;
    public float minSpeed;

    private MovementComponent _movementComponent;
    private float rotationZ;
    private Rigidbody2D rb;
    private Camera mainCam;
    private int _generation;
    private float finalSpeedX;
    private float finalSpeedY;
    private Collider2D collider;

    public virtual void Start()
    {

        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
                
        if (!SetMovementComponent())
        {
            Debug.LogWarning("WARNING! MovementComponent wasn't successfully set\n" +
                             "Asteroid won't be able to move.");
        }
        
        if (maxSpeed < minSpeed)
        {
            Debug.LogWarning("WARNING! max asteroid speed lesser than min speed");
        }
        
        mainCam = Camera.main;
        collider = GetComponent<Collider2D>();
        rotationZ = RandomRotation();

        rb = asteroid.GetComponent<Rigidbody2D>();

        float randomSpeedX = Random.Range(minSpeed, maxSpeed);
        int randomDirectionX = Random.Range(0, 2);
        float dirX = 0;
        if (randomDirectionX == 1) { dirX = -1; }
        else { dirX = 1; }
        finalSpeedX = randomSpeedX * dirX;

        float speedY = Random.Range(minSpeed, maxSpeed);
        int selectorY = Random.Range(0, 2);
        float dirY = 0;
        if (selectorY == 1) { dirY = -1; }
        else { dirY = 1; }
        finalSpeedY = speedY * dirY;

    }

    public virtual float RandomRotation()
    {
        return Random.Range(-maxRotation, maxRotation);
    }

    public bool SetMovementComponent()
    {
        _movementComponent = GetComponent<MovementComponent>();
        if (_movementComponent)
            return true;
        else
            return false;
    }

    public void SetGeneration(int generation)
    {
        _generation = generation;
    }

    void Update()
    {
        asteroid.transform.Rotate(new Vector3(0, 0, rotationZ) * Time.deltaTime);
        CheckPosition();
        _movementComponent.Move(finalSpeedX*Time.deltaTime,finalSpeedY*Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            other.collider.gameObject.GetComponent<Ship>().ShipGotHitted();
        }
    }
    
    public virtual void DestroyAsteroid()
    {
        if (_generation < 3)
        {
            CreateSmallAsteriods(2);
        }

        Destroy(_generation);
    }

    void CreateSmallAsteriods(int asteroidsNum)
    {
        int newGeneration = _generation + 1;
        for (int i = 1; i <= asteroidsNum; i++)
        {
            float scaleSize = 0.5f;
            GameObject AsteroidClone = Instantiate(asteroid, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            AsteroidClone.transform.parent = gameController.transform;
            AsteroidClone.transform.localScale = new Vector3(AsteroidClone.transform.localScale.x * scaleSize, AsteroidClone.transform.localScale.y * scaleSize, AsteroidClone.transform.localScale.z * scaleSize);
            AsteroidClone.GetComponent<AsteroidController>().SetGeneration(newGeneration);
            AsteroidClone.SetActive(true);
        }
    }

    private void CheckPosition()
    {

        Vector2 size = collider.bounds.size/2;
        
        float sceneWidth = mainCam.orthographicSize * 2 * mainCam.aspect;
        float sceneHeight = mainCam.orthographicSize * 2;

        float sceneRightEdge = (sceneWidth / 2);
        float sceneLeftEdge = (sceneRightEdge * -1);
        float sceneTopEdge = (sceneHeight / 2);
        float sceneBottomEdge = (sceneTopEdge * -1);
        

        if (transform.position.x-size.x > sceneRightEdge)
        {
            transform.position = new Vector2(sceneLeftEdge+size.x, transform.position.y);
        }
        if (transform.position.x+size.x < sceneLeftEdge)
        {
            transform.position = new Vector2(sceneRightEdge-size.x, transform.position.y);
        } 
        if (transform.position.y-size.y > sceneTopEdge)
        {
            transform.position = new Vector2(transform.position.x, sceneBottomEdge+size.y);
        }
        if (transform.position.y+size.y < sceneBottomEdge)
        {
            transform.position = new Vector2(transform.position.x, sceneTopEdge-size.y);
        }
    }

    public void Destroy(int generation)
    {
        gameController.AsteroidDestroyed(generation);
        Destroy(gameObject, 0.01f);
    }
    
}
