public class CharacterDataController : APILoader
{
    private CharacterData cData;

    public CharacterDataController(CharacterData characterData)
    {
        cData = characterData;
    }

    public void GetCharacterData()
    {
        base.GetAPI("http://localhost:3000/userInfo", null, cData);
    }

    public void CreateCharacterData()
    {
        base.PostAPI("http://localhost:3000/userInfo/insert", cData);
    }
    
    public void UpdateCharacterData()
    {
        base.PostAPI("http://localhost:3000/userInfo/update", cData);
    }
}