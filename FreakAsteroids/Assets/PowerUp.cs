using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : bullet
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        setDirection(transform.up);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Ship>().powerUp = true;
            DestroyBullet();
        }   
    }
    
}
