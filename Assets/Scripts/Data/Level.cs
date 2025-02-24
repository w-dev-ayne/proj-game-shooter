using System.Collections.Generic;

public class Level
{
    public List<EnemyNetworkData> enemies;
    public List<int> enemiesNums;
    public int totalEnemiesNum;
    public int currentEnemiesNum;

    public Level()
    {
        enemies = new List<EnemyNetworkData>();
        enemiesNums = new List<int>();
    }

    public void AddEnemyData(EnemyNetworkData enemyData)
    {
        enemies.Add(enemyData);
        enemiesNums.Add(enemyData.number);
        
        int amount = 0;
        
        for (int i = 0; i < this.enemiesNums.Count; i++)
        {
            amount += enemiesNums[i];
        }

        totalEnemiesNum = amount;
        currentEnemiesNum = totalEnemiesNum;
    }
}