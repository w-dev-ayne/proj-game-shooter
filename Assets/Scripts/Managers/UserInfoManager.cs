using UnityEngine;

public class UserInfoManager
{
    public UserInfoData data = new UserInfoData();

    // 서버로부터 UserInfo 받아오기
    public async void GetUserInfo()
    {
        GetData<UserInfoNetworkData> response = await Managers.Network.userInfoDataController.GetUserInfo();

        if (response.success)
        {
            this.data.FetchData(response.data);    
        }
    }
}