using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public GameObject gameController;
    public GameObject player;
    public GameObject startGame;


    public void NewGame()
    {
        startGame.SetActive(false);
        Invoke("activateGame",1f); // por efeito aqui depois
    }

    public void activateGame()
    {
        gameController.SetActive(true);
        player.SetActive(true);
    }

    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 0.8f);
    }
}
