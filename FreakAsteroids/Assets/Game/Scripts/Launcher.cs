using UnityEngine;

public abstract class Launcher : MonoBehaviour
{ 
    private IOnLaunch _onLaunch;
    protected abstract void Launch(ShipWeapon shipWeapon);

    protected virtual void Awake()
    {
        _onLaunch = GetComponent<IOnLaunch>();
    }
    protected virtual void OnEnable()
    {
        _onLaunch.OnLaunch += Launch;
    }

    protected virtual void OnDisable()
    {
        _onLaunch.OnLaunch -= Launch;
    }
}