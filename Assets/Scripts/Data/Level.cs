public class Level
{
    public EnemyData[] enemies;
    public int[] enemiesNums;
    public int totalEnemiesNum;

    public Level(LevelData data)
    {
        this.enemies = data.enemies;
        this.enemiesNums = data.enemiesNums;

        int amount = 0;
        
        for (int i = 0; i < this.enemiesNums.Length; i++)
        {
            amount += enemiesNums[i];
        }

        totalEnemiesNum = amount;
    }
}