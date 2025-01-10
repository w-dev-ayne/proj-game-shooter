using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyFactory factory;
    public List<EnemyController> enemies = new List<EnemyController>();
    public List<EnemyController> dirEnemies = new List<EnemyController>();
    
    public EnemyController FindBulletTarget(Vector3 shootPosition, Vector3 direction)
    {
        dirEnemies.Clear();
        foreach (EnemyController enemy in enemies)
        {
            Vector3 enemyDirection = (enemy.transform.position - shootPosition).normalized;
            Debug.Log($"{direction} | {enemyDirection}");
            if (Vector3.Angle(direction, enemyDirection) < 20f)
            {
                dirEnemies.Add(enemy);
            }
        }

        EnemyController nearEnemy = null;
        float distance = 100;

        foreach (EnemyController enemy in dirEnemies)
        {
            if (Vector3.Distance(shootPosition, enemy.transform.position) < distance)
            {
                nearEnemy = enemy;
                distance = Vector3.Distance(shootPosition, enemy.transform.position);
            }
        }

        Debug.Log(nearEnemy);
        return nearEnemy;
    }
}
