using UnityEngine.Events;

public interface IDamageable
{
    public UnityEvent onDamage { get; set; }
    public void TakeDamage(float damage, Bullet bullet = null);
    public bool IsDeadByDamage(float damage);
}