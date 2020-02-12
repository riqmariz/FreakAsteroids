using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreakAsteroid : AsteroidController
{
    // Start is called before the first frame update

    public GameObject powerUp;
    public float life;
    private float _life;
    private Vector3 _localScale;
    public override float RandomRotation()
    {
        int selector = Random.Range(0, 2);
        if (selector == 1)
        {
            return maxRotation;
        }
        else
        {
            return -maxRotation;
        }
    }

    public override void DestroyAsteroid()
    {
        life--;
        
        if (life > 0)
        {
            Resize();
            //call a visual effect
        }
        else
        {
            var pu = Instantiate(powerUp, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            pu.gameObject.GetComponent<PowerUp>().DestroyBulletDelayed();
            Destroy(gameObject, 0.01f);
        }
        
    }

    public void setLife(float num)
    {
        life = num;
        _life = life;
    }

    public void Resize()
    {
        transform.localScale = _localScale * (life/(_life));
    }

    public override void Start()
    {
        base.Start();
        _life = life;
        _localScale = transform.localScale;
    }
    
}
