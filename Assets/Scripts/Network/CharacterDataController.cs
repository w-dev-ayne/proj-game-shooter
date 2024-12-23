public class CharacterDataController : APILoader
{
    private CharacterData cData;

    public CharacterDataController(CharacterData characterData)
    {
        cData = characterData;
    }

    public async void GetCharacterData()
    {
        GetData<CharacterNetworkData> nData = await base.GetAPI<CharacterNetworkData>($"/character", null);
        cData.FetchData(nData.data);
    }

    public void CreateCharacterData()
    {
        base.PostAPI<string>("/insert", cData);
    }
    
    public void UpdateCharacterData()
    {
        base.PostAPI<string>($"/update", cData);
    }
}