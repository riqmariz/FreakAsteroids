﻿using System;
using UnityEngine;

public class ShipHaveHealth : MonoBehaviour, IHaveHealth
{
    [SerializeField]
    private int health = 3;
    public int Health => health;

    public event Action<float> OnHPChanged = delegate { };
    public event Action OnDied = delegate { };
    
    public void TakeDamage(int value)
    {
        health -= value;
        OnHPChanged(health);

        if (health > 0)
        {
            ResetShip();
        }
        else
        {
            Die();
        }
    }

    public void ResetShip() //make it better later
    {
        gameObject.SetActive(false);
        transform.position = new Vector2(0f, 0f);
        transform.eulerAngles = new Vector3(0, 0, 0);
        Invoke("ActivateShip",1.5f);
    }

    public void ActivateShip()
    {
        gameObject.SetActive(true);
    }
    
    public void Die()
    {
        OnDied();
        Destroy(gameObject);
    }
}