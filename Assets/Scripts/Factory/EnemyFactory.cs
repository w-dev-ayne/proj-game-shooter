using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal.Builders;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public GameObject enemyPrefab;

    private const int MAX_SPAWN_DISTANCE = 20;
    private const int MIN_SPAWN_DISTANCE = 5;

    public void GenerateEnemies(Level levelData)
    {
        for (int eIdx = 0; eIdx < levelData.enemies.Length; eIdx++)
        {
            for (int idx = 0; idx < levelData.enemiesNums[eIdx]; idx++)
            {
                int randomX = Random.Range(-MAX_SPAWN_DISTANCE, MAX_SPAWN_DISTANCE);
                int randomY = Random.Range(-MAX_SPAWN_DISTANCE, MAX_SPAWN_DISTANCE);

                if (Mathf.Abs(randomX) <= MIN_SPAWN_DISTANCE)
                {
                    randomX = randomX <= 0 ? randomX - MIN_SPAWN_DISTANCE : randomX + MIN_SPAWN_DISTANCE;
                }
                if (Mathf.Abs(randomY) <= MIN_SPAWN_DISTANCE)
                {
                    randomY = randomY <= 0 ? randomY - MIN_SPAWN_DISTANCE : randomY + MIN_SPAWN_DISTANCE;
                }
            
                GenerateEnemy(levelData.enemies[eIdx], new Vector3(randomX, 1.25f, randomY));    
            }
        }
    }

    private void GenerateEnemy(EnemyData enemyData, Vector3 position)
    {
        EnemyController enemy = Instantiate(enemyPrefab).GetComponent<EnemyController>();
        enemy.transform.position = position;
        enemy.data = enemyData;
        enemy.Initialize();
    }
}
