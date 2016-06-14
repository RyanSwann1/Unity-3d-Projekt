using UnityEngine;
using System.Collections;

public interface IDamageable
{
    void TakeHit(int damage, Vector3 hitPoint, Vector3 impactDirection); //Take position of hit & damage

    void TakeDamage(int damage); //Take damage
}
