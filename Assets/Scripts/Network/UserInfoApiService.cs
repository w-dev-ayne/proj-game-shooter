using System.Threading.Tasks;
using Unity.Cinemachine;

public class UserInfoApiService : APILoader
{
    public async Task<GetData<UserInfoNetworkData>> GetUserInfo()
    {
        GetData<UserInfoNetworkData> response = await base.GetAPI<UserInfoNetworkData>("/userinfo", null);
        return response;
    }

    public async Task<GetData<string>> AddCharacterPoint(AddCharacterPointData data)
    {
        GetData<string> response = await base.PostAPI<string>("/userinfo/add/character", data);
        return response;
    }
    
    public async Task<GetData<string>> AddSkillDrawPoint(AddSkillDrawPointData data)
    {
        GetData<string> response = await base.PostAPI<string>("/userinfo/add/skilldraw", data);
        return response;
    }

    public async Task<GetData<string>> AddCurrentStage()
    {
        GetData<string> response = await base.PostAPI<string>("/userinfo/add/currentStage");
        return response;
    }

    public async Task<GetData<string>> FinishStage(AddCharacterPointData data)
    {
        GetData<string> response = await base.PostAPI<string>("/userinfo/stage/finish", data);
        return response;
    }
}