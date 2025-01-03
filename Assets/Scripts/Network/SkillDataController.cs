using System.Threading.Tasks;
using UnityEngine;

public class SkillDataController : APILoader
{
    public async Task<bool> GetUserSkills()
    {
        GetData<SkillNetworkData[]> response = await base.GetAPI<SkillNetworkData[]>("/skill/user", null); 
        Managers.Skill.FetchSkill(response.data);
        return response.success;
    }

    public async Task<bool> DrawSkill()
    {
        GetData<SkillNetworkData> response =  await base.PostAPI<SkillNetworkData>("/skill/draw");
        Debug.Log(response.data.name);
        return response.success;
    }

    public async Task AddSkill(SkillNetworkData data, NetworkConfig config)
    {
        await base.EditorPostAPI<string>("/admin/skill/add", config, data, false);
    }

    public async Task GetSkillConfiguration()
    {
        GetData<ConfigurationNetworkData[]> response  = await base.GetAPI<ConfigurationNetworkData[]>("/skill/config");
        Managers.Skill.FetchConfigData(response.data);
    }

    public async Task<bool> UpgradeSkill(SkillUpgradeNetworkData data)
    {
        GetData<string> response = await base.PostAPI<string>("/skill/upgrade", data);
        return response.success;
    }
}
