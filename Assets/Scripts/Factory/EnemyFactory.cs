using System.Collections;
using System.Collections.Generic;
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
            
            GenerateEnemy(new Vector3(randomX, 1.25f, randomY));
        }
    }

    private void GenerateEnemy(Vector3 position)
    {
        Instantiate(enemyPrefab).transform.position = position;
    }
}
