using UnityEngine;

public class UserInfoManager
{
    public UserInfoData data = new UserInfoData();

    public async void GetUserInfo()
    {
        UserInfoNetworkData data = await Managers.Network.userInfoDataController.GetUserInfo();
        this.data.FetchData(data);
    }
}