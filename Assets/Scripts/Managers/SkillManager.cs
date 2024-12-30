using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SkillManager
{
    public SkillData[] skills;
    private SkillData[] equippedSkills;
    public SkillUpgradeConfiguration config { get; private set; } = new SkillUpgradeConfiguration();
    

    public void Initialize()
    {
        Managers.Network.skillController.GetUserSkills();
        Managers.Network.skillController.GetSkillConfiguration();
    }

    public async void GetUserSkills(UnityAction onSuccess)
    {
        if (await Managers.Network.skillController.GetUserSkills())
        {
            onSuccess?.Invoke();
        }
    }

    public void FetchSkill(SkillNetworkData[] skills)
    {
        this.skills = new SkillData[skills.Length];
        
        for (int i = 0; i < skills.Length; i++)
        {
            this.skills[i] = new SkillData(skills[i]);
        }
    }

    public void FetchConfigData(ConfigurationNetworkData[] serverConfig)
    {
        this.config.FetchData(serverConfig);
    }

    public async void DrawSkill()
    {
        await Managers.Network.skillController.DrawSkill();
    }

    public async void UpgradeSkill(SkillUpgradeNetworkData data)
    {
        bool success = await Managers.Network.skillController.UpgradeSkill(data);
        
        if (success)
        {
            GetUserSkills(() =>
            {
                Managers.UI.FindPopup<UI_SkillManagement>().LoadSkillData();
                Managers.UI.FindPopup<UI_SkillManagement>().OnClickSkillButton();
            });
        }
    }
    
    
}
