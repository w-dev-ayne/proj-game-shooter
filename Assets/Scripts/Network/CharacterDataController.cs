using System.Threading.Tasks;
using UnityEditor.VersionControl;

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

    public async Task<bool> UpdateCharacterUpgradeData(CharacterUpgradeNetworkData data)
    {
        GetData<string> response = await base.PostAPI<string>("/character/upgrade", data);
        return response.success;
    }
}