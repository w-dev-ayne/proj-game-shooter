[System.Serializable]
public class UserInfoData
{
    public int id;
    public int level;
    public int skillPoint;
    public int characterPoint;
    public int skilldrawPoint;
    public int currentStage;

    public void FetchData(UserInfoNetworkData data)
    {
        id = data.id;
        level = data.level;
        skillPoint = data.skillPoint;
        characterPoint = data.characterPoint;
        skilldrawPoint = data.skilldrawPoint;
        currentStage = data.currentStage;
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
    public int currentStage;
}

[System.Serializable]
public class AddCharacterPointData : PostData
{
    public int amount;

    public AddCharacterPointData(int amount)
    {
        this.amount = amount;
    }
}

[System.Serializable]
public class AddSkillDrawPointData : PostData
{
    public int amount;

    public AddSkillDrawPointData(int amount)
    {
        this.amount = amount;
    }
}