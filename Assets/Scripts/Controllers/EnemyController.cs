using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    public float hp = 10;
    
    public void TakeDamage(float damage)
    {
        Debug.Log("Take Damage");
    }
}