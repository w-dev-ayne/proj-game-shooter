using System;
using System.Collections;
using CartoonFX;
using UnityEngine;

public class Bullet : PooledObject
{
    public ParticleSystem hitParticle;
    public GameObject renderer;
    private float damage;

    private void OnEnable()
    {
        this.transform.localScale = Vector3.one / 2;
        renderer.SetActive(true);
        
        Invoke("Release", 5.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            BulletPool bPool = pool as BulletPool;
            bPool.CameraShake();
            
            renderer.SetActive(false);
            Invoke("Release", hitParticle.duration);
            hitParticle.Play();
            StopAllCoroutines(); 
            damageable.TakeDamage(this.damage);
        }
    }

    public void Shoot(Vector3 direction, float speed, float damage)
    {
        this.damage = damage;
        StartCoroutine(CoShoot(direction, speed));
    }

    private IEnumerator CoShoot(Vector3 direction, float speed)
    {
        WaitForEndOfFrame oneFrame = new WaitForEndOfFrame();

        while (true)
        {
            this.transform.position += direction * 30 * speed * Time.deltaTime;
            yield return oneFrame;
        }
    }
}