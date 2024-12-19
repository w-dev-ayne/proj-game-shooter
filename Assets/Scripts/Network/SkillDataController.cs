using System.Threading.Tasks;
using UnityEngine;

public class SkillDataController : APILoader
{
    public SkillsData skillsData = new SkillsData();
    public SkillData skillData = new SkillData();
    
    public void GetUserSkills()
    {
        base.GetAPI<string>("/api", null);
    }

    public async Task DrawSkill()
    {
        GetData<SkillNetworkData> response =  await base.PostAPI<SkillNetworkData>("/skill/draw");
        response.Print();
        Debug.Log(response.data.name);
    }

    public void UpgradeSkill()
    {
        
    }
}
