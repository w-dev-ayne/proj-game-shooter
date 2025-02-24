using System.Threading.Tasks;

public class EnemyDataController : APILoader
{
    public async Task<EnemyNetworkData[]> GetEnemiesData(int stageId)
    {
        GetData<EnemyNetworkData[]> response = await base.GetAPI<EnemyNetworkData[]>($"/admin/enemy/list/{stageId}", null);
        return response.data;
    }
}