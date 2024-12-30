using System.Threading.Tasks;

public class CharacterDataController : APILoader
{
    public async void GetCharacterData()
    {
        GetData<CharacterNetworkData> nData = await base.GetAPI<CharacterNetworkData>($"/character", null);
        Managers.Character.FetchData(nData.data);
    }

    public void CreateCharacterData()
    {
        //base.PostAPI<string>("/insert", cData);
    }
    
    public void UpdateCharacterData()
    {
        //base.PostAPI<string>($"/update", cData);
    }

    public async Task<bool> UpgradeCharacter(CharacterUpgradeNetworkData data)
    {
        GetData<string> response = await base.PostAPI<string>("/character/upgrade", data);
        return response.success;
    }

    public async Task GetCharacterUpgradeConfigurationData()
    {
        GetData<ConfigurationNetworkData[]> response = await base.GetAPI<ConfigurationNetworkData[]>($"/character/config");
        Managers.Character.FetchConfigData(response.data);
    }
}