using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Ship : MonoBehaviour
{
    public GameObject firepoint;
    public GameObject bullet;
    public GameObject poweredBullet;
    public float poweredBulletDegree;
    public float shipSpeed;
    public float rotationSpeed = 180f;

    private Camera mainCam;
    private MovementComponent _movementComponent;
    private GameController _gameController;
    private int lives = 3;
    [NonSerialized] public int score = 0;
    public bool powerUp;
    private bool firstPowerUp;


    private bool _shoot;
    private bool _thrust;
    private bool _rotate;
    private float _inputHorizontal;

    private void Start()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        mainCam = Camera.main;
        bullet.SetActive(false);
        if (!SetMovementComponent())
        {
            Debug.LogWarning("WARNING! MovementComponent wasn't successfully set\n" +
                             "Actor won't be able to move.");
        }

        ResetShip();
    }

    private void FixedUpdate()
    {
        CheckPosition();

        Shoot();

        ControlShipRotation();

        Thrust();
    }

    private void Thrust()
    {
        if (_thrust)
        {
            Vector2 dir = transform.up;
            _movementComponent.Move(dir.x * shipSpeed * Time.fixedDeltaTime, dir.y * shipSpeed * Time.fixedDeltaTime);
            _thrust = false;
        }
    }

    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        if (Input.GetKeyDown("space"))
        {
            _shoot = true;
        }

        //float vertical = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _thrust = true;
        }

        _inputHorizontal =  Input.GetAxis("Horizontal");
    }

    public void ShipGotHitted()
    {
        lives--;
        _gameController.RemoveLife(lives);
        gameObject.SetActive(false);
        ResetShip();
        if (lives == 0)
        {
            _gameController.Finish();
        }
        else
        {
            Invoke("activateShip", 1.5f);
        }
    }

    public void activateShip()
    {
        gameObject.SetActive(true);
    }

    public bool SetMovementComponent()
    {
        _movementComponent = GetComponent<MovementComponent>();
        if (_movementComponent)
            return true;
        else
            return false;
    }

    private void ControlShipRotation()
    {
        transform.Rotate(0, 0, _inputHorizontal * rotationSpeed * Time.fixedDeltaTime);
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

        if (transform.position.x < sceneLeftEdge)
        {
            transform.position = new Vector2(sceneRightEdge, transform.position.y);
        }

        if (transform.position.y > sceneTopEdge)
        {
            transform.position = new Vector2(transform.position.x, sceneBottomEdge);
        }

        if (transform.position.y < sceneBottomEdge)
        {
            transform.position = new Vector2(transform.position.x, sceneTopEdge);
        }
    }

    public void ResetShip()
    {
        transform.position = new Vector2(0f, 0f);
        transform.eulerAngles = new Vector3(0, 180f, 0);
        _movementComponent.StopMovement();
        powerUp = false;
        firstPowerUp = false;
    }

    public void ResetLives()
    {
        lives = 3;
    }

    void Shoot()
    {
        if (_shoot)
        {
            if (!powerUp)
            {
                GameObject Bullet = Instantiate(bullet,
                    new Vector2(firepoint.transform.position.x, firepoint.transform.position.y), transform.rotation);
                Bullet.SetActive(true);
                bullet script = Bullet.GetComponent<bullet>();
                script.SetPlayerShip(this);
                script.DestroyBulletDelayed();
            }
            else
            {
                if (!firstPowerUp)
                {
                    Invoke("deactivatePowerUp", 10f);
                    firstPowerUp = true;
                }

                GameObject Bullet = Instantiate(poweredBullet,
                    new Vector2(firepoint.transform.position.x, firepoint.transform.position.y), transform.rotation);
                Bullet.SetActive(true);
                bullet script = Bullet.GetComponent<bullet>();
                script.SetPlayerShip(this);
                script.DestroyBulletDelayed();

                GameObject BulletEsq = Instantiate(poweredBullet,
                    new Vector2(firepoint.transform.position.x, firepoint.transform.position.y), transform.rotation);
                bullet scriptEsq = BulletEsq.GetComponent<bullet>();
                scriptEsq.ChangeDirection(poweredBulletDegree);
                scriptEsq.SetPlayerShip(this);
                scriptEsq.DestroyBulletDelayed();
                BulletEsq.SetActive(true);

                GameObject BulletDir = Instantiate(poweredBullet,
                    new Vector2(firepoint.transform.position.x, firepoint.transform.position.y), transform.rotation);
                bullet scriptDir = BulletDir.GetComponent<bullet>();
                scriptDir.ChangeDirection(-poweredBulletDegree);
                scriptDir.SetPlayerShip(this);
                scriptDir.DestroyBulletDelayed();
                BulletDir.SetActive(true);
            }
            
            
            _shoot = false;
        }
    }

    public void deactivatePowerUp()
    {
        powerUp = false;
        firstPowerUp = false;
    }
}