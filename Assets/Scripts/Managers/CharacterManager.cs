public class CharacterManager
{
    public CharacterData data { get; private set; } = new CharacterData();
    
    public void FetchData(CharacterNetworkData data)
    {
        this.data.FetchData(data);
    }
}