using System.Threading.Tasks;

public class EnemyDataController : APILoader
{
    public async Task<bool> GetEnemiesData()
    {
        GetData<EnemyNetworkData[]> response = await base.GetAPI<EnemyNetworkData[]>("endpoint/param", null);
        return response.success;
    }
}