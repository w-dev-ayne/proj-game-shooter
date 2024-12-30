using UnityEngine.Events;

public class CharacterManager
{
    public CharacterData data { get; private set; } = new CharacterData();
    public CharacterUpgradeConfiguration config { get; private set; } = new CharacterUpgradeConfiguration();

    public void Initialize()
    {
        Managers.Network.cDataController.GetCharacterData();
        Managers.Network.cDataController.GetCharacterUpgradeConfigurationData();
    }

    public async void GetCharacterData(UnityAction onSuccess = null)
    {
        if (await Managers.Network.cDataController.GetCharacterData())
        {
            onSuccess?.Invoke();
        }
    }
    
    public void FetchData(CharacterNetworkData data)
    {
        this.data.FetchData(data);
    }

    public void FetchConfigData(ConfigurationNetworkData[] serverConfig)
    {
        config.FetchData(serverConfig);
    }

    public async void UpgradeData(CharacterUpgradeNetworkData data)
    {
        bool success = await Managers.Network.cDataController.UpgradeCharacter(data);

        if (success)
        {
            Managers.UserInfo.GetUserInfo();
            GetCharacterData();
        }
    }
}