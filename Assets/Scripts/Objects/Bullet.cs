using System;
using System.Collections;
using CartoonFX;
using UnityEngine;

public class Bullet : PooledObject
{
    public GameObject renderer;
    public Collider collider;
    private Rigidbody rb;
    private float damage;
    private EnemyController target;

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
            /*BulletPool bPool = pool as BulletPool;
            bPool.CameraShake();*/
            
            renderer.SetActive(false);
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
        this.damage = damage;
        this.target = Managers.Enemy.FindBulletTarget(this.transform.position, direction);
        target?.Highlight();
        StartCoroutine(CoShoot(direction, speed, Managers.Stage.cc.bulletAccuracy, target));
    }

    private IEnumerator CoShoot(Vector3 direction, float speed, float accuracy, EnemyController target = null)
    {
        WaitForEndOfFrame oneFrame = new WaitForEndOfFrame();
        Vector3 moveDirection = direction;
        Vector3 accuracyDirection = Vector3.zero; 
        
        while (true)
        {
            moveDirection = direction;
            if (target != null)
            {
                accuracyDirection = (target.transform.position - this.transform.position).normalized;
                accuracyDirection.y = 0;
                moveDirection = Vector3.Lerp(direction, accuracyDirection, 0.3f * accuracy).normalized;
            }
            this.rb.MovePosition(this.transform.position + moveDirection * FactorDefine.BULLET_SPEED * speed * Time.deltaTime);
            yield return oneFrame;
        }
    }
}