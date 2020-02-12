using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed;
    public float destroyDelay;
    private Vector3 direction;
    private MovementComponent _movementComponent;
    private Camera mainCam;
    private Ship playerShip;

    void Start()
    {
        if (!SetMovementComponent())
            Debug.LogWarning("WARNING! MovementComponent wasn't successfully set\n" +
                             "Actor won't be able to move.");
        
        direction = transform.up;
        mainCam = Camera.main;
    }
    
    void Update()
    {
        _movementComponent.Move(direction.x * speed * Time.deltaTime,direction.y * speed * Time.deltaTime);
        //transform.position += direction * speed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        CheckPosition();
    }

    private void CheckPosition()
    {

        float sceneWidth = mainCam.orthographicSize * 2 * mainCam.aspect;
        float sceneHeight = mainCam.orthographicSize * 2;

        float sceneRightEdge = sceneWidth / 2;
        float sceneLeftEdge = sceneRightEdge * -1;
        float sceneTopEdge = sceneHeight / 2;
        float sceneBottomEdge = sceneTopEdge * -1;

        if (transform.position.x > sceneRightEdge)
        {
            transform.position = new Vector2(sceneLeftEdge, transform.position.y);
        }
        if (transform.position.x < sceneLeftEdge) { transform.position = new Vector2(sceneRightEdge, transform.position.y); } if (transform.position.y > sceneTopEdge)
        {
            transform.position = new Vector2(transform.position.x, sceneBottomEdge);
        }
        if (transform.position.y < sceneBottomEdge)
        {
            transform.position = new Vector2(transform.position.x, sceneTopEdge);
        }
    }
    public bool SetMovementComponent()
    {
        _movementComponent = GetComponent<MovementComponent>();
        if (_movementComponent)
            return true;
        else
            return false;
    }

    public void SetPlayerShip(Ship ship)
    {
        playerShip = ship;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            playerShip.score = playerShip.score + 100;
            other.gameObject.GetComponent<AsteroidController>().DestroyAsteroid();
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject, 0.0f);
    }

    public void DestroyBulletDelayed()
    {
        Destroy(gameObject,destroyDelay);
    }
    
}
