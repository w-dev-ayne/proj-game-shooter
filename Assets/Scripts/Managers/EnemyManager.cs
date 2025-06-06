using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyFactory factory;
    public List<GameObject> enemyPrefabs = new List<GameObject>();
    public Dictionary<string, GameObject> EnemyPrefabDictionary = new Dictionary<string, GameObject>();
    public List<EnemyController> enemies = new List<EnemyController>();
    public List<EnemyController> dirEnemies = new List<EnemyController>();

    void Awake()
    {
        foreach (GameObject prefab in enemyPrefabs)
        {
            EnemyPrefabDictionary.Add(prefab.name, prefab);
        }
    }
    
    public async void GetEnemiesData()
    {
        EnemyNetworkData[] enemyData = await Managers.Network.EnemyApiService.GetEnemiesData(Managers.UserInfo.data.currentStage);
        Managers.Stage.InsertEnemies(enemyData);
        
    }
    
    public EnemyController FindBulletTarget(Vector3 shootPosition, Vector3 direction)
    {
        dirEnemies.Clear();
        foreach (EnemyController enemy in enemies)
        {
            Vector3 enemyDirection = (enemy.transform.position - shootPosition).normalized;
            enemyDirection.y = 0;
            if (Vector3.Angle(direction, enemyDirection) < 30f)
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
        
        return nearEnemy;
    }

    public void TakeOffEnemy(EnemyController enemy)
    {
        if (this.enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }

        if (this.dirEnemies.Contains(enemy))
        {
            dirEnemies.Remove(enemy);
        }
    }
    
}
