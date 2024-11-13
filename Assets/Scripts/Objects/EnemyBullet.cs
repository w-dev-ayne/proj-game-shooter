using System.Collections;
using UnityEngine;

public class EnemyBullet : PooledObject
{
    public ParticleSystem hitParticle;
    public GameObject renderer;
    private float damage = 0;

    private void OnEnable()
    {
        this.transform.localScale = Vector3.one / 3;
        this.transform.localPosition = Vector3.zero;
        renderer.GetComponent<Renderer>().enabled = true;
        Invoke("Release", 5.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            if (!other.CompareTag("Player"))
                return;
            renderer.GetComponent<Renderer>().enabled = false;
            Invoke("Release", hitParticle.duration);
            hitParticle.Play();
            StopAllCoroutines(); 
            damageable.TakeDamage(damage);
        }
        else
        {
            if (other.CompareTag("Bullet"))
                return;
            Release();
        }
    }

    public void Shoot(Vector3 targetPosition, float speed, float damage)
    {
        this.damage = damage;
        this.transform.position = pool.transform.position;
        StartCoroutine(CoShoot(targetPosition, speed));
    }

    private IEnumerator CoShoot(Vector3 targetPosition, float speed)
    {
        Vector3 direction = targetPosition - pool.transform.position;
        direction = new Vector3(direction.x, 0, direction.z).normalized;
        
        WaitForEndOfFrame oneFrame = new WaitForEndOfFrame();

        while (Vector3.Distance(this.transform.position, targetPosition) > 0.01f)
        {
            this.transform.position += direction * 10 * speed * Time.deltaTime;
            yield return oneFrame;
        }

        Release();
    }
}
