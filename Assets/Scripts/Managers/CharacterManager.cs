using UnityEngine.Events;

public class CharacterManager
{
    public CharacterData data { get; private set; } = new CharacterData();
    public CharacterUpgradeConfiguration config { get; private set; } = new CharacterUpgradeConfiguration();

    public void Initialize()
    {
        Managers.Network.CApiService.GetCharacterData();
        Managers.Network.CApiService.GetCharacterUpgradeConfigurationData();
    }

    // 서버로부터 캐릭터 정보 받아오기
    public async void GetCharacterData(UnityAction onSuccess = null)
    {
        if (await Managers.Network.CApiService.GetCharacterData())
        {
            onSuccess?.Invoke();
        }
    }
    
    // 서버데이터로부터 로컬 캐릭터 데이터 동기화
    public void FetchData(CharacterNetworkData data)
    {
        this.data.FetchData(data);
    }

    // 서버데이터로부터 로컬 캐릭터 업그레이드 설정 데이터 동기화
    public void FetchConfigData(ConfigurationNetworkData[] serverConfig)
    {
        config.FetchData(serverConfig);
    }

    // 캐릭터 데이터 업그레이드 서버에 호출
    public async void UpgradeData(CharacterUpgradeNetworkData data)
    {
        bool success = await Managers.Network.CApiService.UpgradeCharacter(data);

        if (success)
        {
            Managers.UserInfo.GetUserInfo();
            GetCharacterData();
        }
    }
}