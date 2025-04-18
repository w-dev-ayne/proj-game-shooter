using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class UserInfoManager
{
    public UserInfoData data = new UserInfoData();

    // 서버로부터 UserInfo 받아오기
    public async void GetUserInfo(UnityAction onComplete = null)
    {
        GetData<UserInfoNetworkData> response = await Managers.Network.UserInfoApiService.GetUserInfo();

        if (response.success)
        {
            this.data.FetchData(response.data);   
            onComplete?.Invoke();
        }
    }

    public async void AddCharacterPoint(int amount)
    {
        AddCharacterPointData data = new AddCharacterPointData(amount);
        GetData<string> response = await Managers.Network.UserInfoApiService.AddCharacterPoint(data);

        if (response.success)
        {
            GetUserInfo();
        }
    }

    public async void AddSkillDrawPoint()
    {
        AddSkillDrawPointData data = new AddSkillDrawPointData(1);
        GetData<string> response = await Managers.Network.UserInfoApiService.AddSkillDrawPoint(data);

        if (response.success)
        {
            GetUserInfo();
        }
    }

    public async Task FinishStage(int amount)
    {
        AddCharacterPointData cpData = new AddCharacterPointData(amount);

        GetData<string> response = await Managers.Network.UserInfoApiService.FinishStage(cpData);

        if (response.success)
        {
            GetUserInfo();
        }
    }
}