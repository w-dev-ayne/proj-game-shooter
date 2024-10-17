using System;
using UnityEngine;

public class Bullet : PooledObject
{
    public float attack = 0;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakeDamage(attack);
            Release();
        }
        // Return To Pool 구현
    }
}