using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FreakAsteroid2 : Asteroid2
{
    private int _maxHitsPoints;
    private Vector3 _originalLocalScale;
    protected override void Start()
    {
        base.Start();
        _maxHitsPoints = Health;
        _originalLocalScale = transform.localScale;
    }

    public override void TakeDamage(int value)
    {
        base.TakeDamage(value);
        if (Health > 0)
        {
            Resize(value);
        }
    }

    protected override void DestroyAsteroid()
    {
       // ReleasePowerUp();
        
        Destroy(gameObject,0.01f);
    }

    private void ReleasePowerUp()
    {
        throw new System.NotImplementedException();
    }

    private void Resize(int value)
    {
        var minPctSize = 0.30f;
        var pct = 1.0f;
        var damagePerHealth = value / (_maxHitsPoints*(pct+minPctSize));

        var damageScale = _originalLocalScale * damagePerHealth;

        transform.localScale = transform.localScale - (damageScale);
    }

    protected override float GetAsteroidRotation()
    {
        return maxRotation;
    }
    
    
}