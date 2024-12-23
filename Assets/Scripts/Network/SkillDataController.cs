using System.Threading.Tasks;
using UnityEngine;

public class SkillDataController : APILoader
{
    public async Task GetUserSkills()
    {
        GetData<SkillNetworkData[]> response = await base.GetAPI<SkillNetworkData[]>("/skill/user", null); 
        Debug.Log(response.data.Length);
        Managers.Skill.FetchSkill(response.data);
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
