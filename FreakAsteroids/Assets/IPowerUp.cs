using UnityEngine;

internal interface IPowerUp
{
    void Drop(Vector3 direction);
    GameObject Target { get; set; }
    float Duration { get; set; }
    
    void Apply();

    void Remove();
}