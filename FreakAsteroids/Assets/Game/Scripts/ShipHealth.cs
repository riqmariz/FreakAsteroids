using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealth : MonoBehaviour, IHaveHealth
{
    [SerializeField]
    private int health = 3;
    public int Health => health;

    [SerializeField]
    private float invulnerabilityTimeAfterEachHit = 2f;

    private bool _canTakeDamage=true;
    
    
    public event Action<float> OnHPChanged = delegate { };
    public event Action OnDied = delegate { };
    
    public void TakeDamage(int value)
    {
        if (_canTakeDamage)
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
    }
    
    public void ResetShip() //make it better later
    {
        gameObject.SetActive(false);
        transform.position = new Vector2(0f, 0f);
        transform.eulerAngles = new Vector3(0, 0, 0);
        Invoke("ActivateShipAfterReset",1.5f);
    }

    public void ActivateShipAfterReset()
    {
        gameObject.SetActive(true);
        StartCoroutine(InvulnerabilityTimer());
    }
    private IEnumerator InvulnerabilityTimer()
    {
        _canTakeDamage = false;
        yield return new WaitForSeconds(invulnerabilityTimeAfterEachHit);
        _canTakeDamage = true;
    }
    
    public void Die()
    {
        OnDied();
        Destroy(gameObject);
    }
}