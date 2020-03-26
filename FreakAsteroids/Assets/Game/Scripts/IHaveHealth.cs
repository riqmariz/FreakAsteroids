using System;
using System.Numerics;
using UnityEngine.Experimental.GlobalIllumination;

internal interface IHaveHealth : ITakeDamage
{
    int Health { get; }
    event System.Action<float> OnHPChanged;
    event System.Action OnDied;
}