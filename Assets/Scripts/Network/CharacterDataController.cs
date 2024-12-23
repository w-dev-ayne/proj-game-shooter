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
}