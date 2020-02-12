using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public int startAsteroids;
    public int increaseAsteroidsPerLevel;
    public GameObject asteroid;
    public int minStartPositionXAsteroid;
    public int maxStartPositionXAsteroid;
    public float startPositionYAsteroid;

    public TextMeshProUGUI scoreInGame;
    public GameObject finish;
    public GameObject lives;

    private int currentNumAsteroids;
    private int totalNumAsteroids;
    private bool levelUp;
    private bool asteroidsCreated;
    private Ship ship;

    void Start()
    {
        CreateAsteroids(startAsteroids);
        totalNumAsteroids = startAsteroids;
        ship = GameObject.FindWithTag("Player").GetComponent<Ship>();
        scoreInGame.gameObject.SetActive(true);
        for(int i=0;i<transform.childCount;i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void Replay()
    {
        finish.SetActive(false);

        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        
        ship.ResetShip();
        ship.ResetLives();
        ship.score = 0;
        scoreInGame.gameObject.SetActive(true);
        ship.gameObject.SetActive(true);
        
        for(int i=0;i<lives.transform.childCount;i++)
        {
            lives.transform.GetChild(i).gameObject.SetActive(true);
        }
        
        CreateAsteroids(startAsteroids);
        totalNumAsteroids = startAsteroids;
    }

    private void CreateAsteroids(int numAsteroids)
    {
        for (int i = 0; i < numAsteroids; i++)
        {
            GameObject astrd = Instantiate(asteroid, new Vector2(Random.Range(minStartPositionXAsteroid, maxStartPositionXAsteroid),startPositionYAsteroid), transform.rotation);
            astrd.transform.parent = transform;
            astrd.GetComponent<AsteroidController>().SetGeneration(1);
            astrd.SetActive(true);
        }
        currentNumAsteroids = numAsteroids;
        asteroidsCreated = true;
    }

    private bool isAllAsteroidsDestroyed()
    {
        if (currentNumAsteroids == 0)
        {
            levelUp = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void LevelUp()
    {
        CreateAsteroids(totalNumAsteroids);
    }
    
    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 0.8f);

        if (isAllAsteroidsDestroyed())
        {
            if (levelUp && asteroidsCreated)
            {
                asteroidsCreated = false;
                totalNumAsteroids = totalNumAsteroids + increaseAsteroidsPerLevel;
                currentNumAsteroids = -1;
                Invoke("LevelUp",1f);
                levelUp = false;
            }
        }

    }

    public void Finish()
    {
        scoreInGame.gameObject.SetActive(false);
        finish.SetActive(true);
        finish.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Score: "+ship.score;
    }

    public void RemoveLife(int lives)
    {
        this.lives.transform.GetChild(lives).gameObject.SetActive(false);
    }
    
    private void FixedUpdate()
    {
        scoreInGame.text = "Score: "+ship.score;
    }

    public void AsteroidDestroyed(int generation)
    {
        if (generation < 3)
        {
            currentNumAsteroids = currentNumAsteroids + 2;
        }
        
        currentNumAsteroids--;
    }

}
