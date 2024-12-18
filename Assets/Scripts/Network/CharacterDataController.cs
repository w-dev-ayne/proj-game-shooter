public class CharacterDataController : APILoader
{
    private CharacterData cData;

    public CharacterDataController(CharacterData characterData)
    {
        cData = characterData;
    }

    public void GetCharacterData()
    {
        base.GetAPI($"/userInfo", null, cData);
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