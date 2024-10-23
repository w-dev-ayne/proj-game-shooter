using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal.Builders;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public GameObject enemyPrefab;


    public void GenerateEnemies()
    {
        for (int i = 0; i < 10; i++)
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
            
            GenerateEnemy(new Vector3(randomX, 1.25f, randomY));
        }
    }

    private void GenerateEnemy(Vector3 position)
    {
        Instantiate(enemyPrefab).transform.position = position;
    }
}
