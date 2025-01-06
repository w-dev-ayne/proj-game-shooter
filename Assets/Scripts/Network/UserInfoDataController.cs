using System.Threading.Tasks;
using Unity.Cinemachine;

public class UserInfoDataController : APILoader
{
    public async Task<GetData<UserInfoNetworkData>> GetUserInfo()
    {
        GetData<UserInfoNetworkData> response = await base.GetAPI<UserInfoNetworkData>("/userinfo", null);
        return response;
    }
}