using UnityEngine;

internal interface IPowerUp
{
    void Drop(Vector3 direction);

    void Buff(GameObject player);
}