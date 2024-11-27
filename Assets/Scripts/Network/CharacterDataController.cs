public class CharacterDataController : APILoader
{
    private CharacterData cData;

    public CharacterDataController(CharacterData characterData)
    {
        cData = characterData;
    }

    public void GetCharacterData()
    {
        base.GetAPI($"{NetworkDefine.Host}/userInfo", null, cData);
    }

    public void CreateCharacterData()
    {
        base.PostAPI($"{NetworkDefine.Host}/insert", cData);
    }
    
    public void UpdateCharacterData()
    {
        base.PostAPI($"{NetworkDefine.Host}/update", cData);
    }
}