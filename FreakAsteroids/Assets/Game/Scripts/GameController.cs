using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int startAsteroids;
    public int increaseAsteroidsPerLevel;
    public GameObject asteroid;
    public int minStartPositionXAsteroid;
    public int maxStartPositionXAsteroid;
    public float startPositionYAsteroid;

    private int currentNumAsteroids;
    private int totalNumAsteroids;
    private bool levelUp;
    private bool asteroidsCreated;
    
    void Start()
    {
        CreateAsteroids(startAsteroids);
        totalNumAsteroids = startAsteroids;
    }

    private void CreateAsteroids(int numAsteroids)
    {
        for (int i = 0; i < numAsteroids; i++)
        {
            GameObject astrd = Instantiate(asteroid, new Vector2(Random.Range(minStartPositionXAsteroid, maxStartPositionXAsteroid),startPositionYAsteroid), transform.rotation);
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

    public void AsteroidDestroyed(int generation)
    {
        if (generation < 3)
        {
            currentNumAsteroids = currentNumAsteroids + 2;
        }
        
        currentNumAsteroids--;
    }

}
