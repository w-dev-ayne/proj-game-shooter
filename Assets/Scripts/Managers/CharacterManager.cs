public class CharacterManager
{
    public CharacterData data { get; private set; } = new CharacterData();

    public void GetCharacterData()
    {
        Managers.Network.cDataController.GetCharacterData();
    }
    
    public void FetchData(CharacterNetworkData data)
    {
        this.data.FetchData(data);
    }

    public async void UpgradeData(CharacterUpgradeNetworkData data)
    {
        bool success = await Managers.Network.cDataController.UpdateCharacterUpgradeData(data);

        if (success)
        {
            Managers.UserInfo.GetUserInfo();
            GetCharacterData();
        }
    }
}