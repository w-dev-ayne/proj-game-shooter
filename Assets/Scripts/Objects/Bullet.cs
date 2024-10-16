using System;
using UnityEngine;

public class Bullet : PooledObject
{
    private void OnTriggerEnter(Collider other)
    {
        throw new NotImplementedException();
        // Return To Pool 구현
    }
}