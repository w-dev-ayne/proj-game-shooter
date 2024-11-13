using System;
using System.Collections;
using CartoonFX;
using UnityEngine;

public class Bullet : PooledObject
{
    public ParticleSystem hitParticle;
    public GameObject renderer;
    public Collider collider;
    private Rigidbody rb;
    private float damage;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        this.transform.localScale = Vector3.one / 2;
        renderer.SetActive(true);
        collider.enabled = true;
        Invoke("Release", 5.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            
            
            renderer.SetActive(false);
            Invoke("Release", hitParticle.duration);
            hitParticle.Play();
            collider.enabled = false;
            StopAllCoroutines(); 
            damageable.TakeDamage(this.damage);
        }
        else
        {
            Release();
        }
    }

    public void Shoot(Vector3 direction, float speed, float damage)
    {
        BulletPool bPool = pool as BulletPool;
        bPool.CameraShake();
        this.damage = damage;
        StartCoroutine(CoShoot(direction, speed));
    }

    private IEnumerator CoShoot(Vector3 direction, float speed)
    {
        WaitForEndOfFrame oneFrame = new WaitForEndOfFrame();

        while (true)
        {
            this.rb.MovePosition(this.transform.position + direction * 30 * speed * Time.deltaTime);
            // this.transform.position += direction * 30 * speed * Time.deltaTime;
            yield return oneFrame;
        }
    }
}