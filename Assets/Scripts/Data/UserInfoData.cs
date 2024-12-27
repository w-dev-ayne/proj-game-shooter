[System.Serializable]
public class UserInfoData
{
    public int id;
    public int level;
    public int skillPoint;
    public int characterPoint;
    public int skilldrawPoint;

    public void FetchData(UserInfoNetworkData data)
    {
        id = data.id;
        level = data.level;
        skillPoint = data.skillPoint;
        characterPoint = data.characterPoint;
        skilldrawPoint = data.skilldrawPoint;
    }
}

[System.Serializable]
public class UserInfoNetworkData : PostData
{
    public int id;
    public int level;
    public int skillPoint;
    public int characterPoint;
    public int skilldrawPoint;
}