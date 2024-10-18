using System;
using System.Collections;
using UnityEngine;

public class Bullet : PooledObject
{
    public float attack = 0;
    public ParticleSystem hitParticle;

    private void OnEnable()
    {
        this.transform.localScale = Vector3.one / 2;
        
        Invoke("Release", 5.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakeDamage(attack);
            hitParticle.Play();
            StopAllCoroutines();

            Invoke("Release", hitParticle.duration);
            
            //Release();
        }
        // Return To Pool 구현
    }

    public void Shoot(Vector3 direction)
    {
        StartCoroutine(CoShoot(direction));
    }

    private IEnumerator CoShoot(Vector3 direction)
    {
        WaitForEndOfFrame oneFrame = new WaitForEndOfFrame();

        while (true)
        {
            this.transform.position += direction * 30 * Time.deltaTime;
            yield return oneFrame;
        }
    }
}