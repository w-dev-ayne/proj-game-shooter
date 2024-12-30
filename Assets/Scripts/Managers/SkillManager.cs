using Unity.VisualScripting;
using UnityEngine;

public class SkillManager
{
    public SkillData[] skills;
    private SkillData[] equippedSkills;
    private SkillUpgradeConfiguration config = new SkillUpgradeConfiguration();

    public void Initialize()
    {
        Managers.Network.skillController.GetUserSkills();
        Managers.Network.skillController.GetSkillConfiguration();
    }

    public void FetchSkill(SkillNetworkData[] skills)
    {
        this.skills = new SkillData[skills.Length];
        
        for (int i = 0; i < skills.Length; i++)
        {
            this.skills[i] = new SkillData(skills[i]);
        }
    }

    public async void FetchConfigData(ConfigurationNetworkData[] serverConfig)
    {
        this.config.FetchData(serverConfig);
    }

    public async void DrawSkill()
    {
        await Managers.Network.skillController.DrawSkill();
    }

    public void UpgradeSkill()
    {
        //dataController.UpgradeSkill();
    }
    
}
