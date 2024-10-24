using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal.Builders;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public GameObject enemyPrefab;

    public void GenerateEnemies(Level levelData)
    {
        for (int eIdx = 0; eIdx < levelData.enemies.Length; eIdx++)
        {
            for (int idx = 0; idx < levelData.enemiesNums[eIdx]; idx++)
            {
                int randomX = Random.Range(-50, 50);
                int randomY = Random.Range(-50, 50);

                if (Mathf.Abs(randomX) <= 20)
                {
                    randomX = randomX <= 0 ? randomX - 20 : randomX + 20;
                }
                if (Mathf.Abs(randomY) <= 20)
                {
                    randomY = randomY <= 0 ? randomY - 20 : randomY + 20;
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
