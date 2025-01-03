using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SkillManager
{
    public SkillData[] skills;
    public SkillData[] equippedSkills = new SkillData[4];
    public SkillUpgradeConfiguration config { get; private set; } = new SkillUpgradeConfiguration();
    

    public void Initialize()
    {
        Managers.Network.skillController.GetUserSkills();
        Managers.Network.skillController.GetSkillConfiguration();
    }

    public async void GetUserSkills(UnityAction onSuccess = null)
    {
        if (await Managers.Network.skillController.GetUserSkills())
        {
            onSuccess?.Invoke();
        }
    }

    public void FetchSkill(SkillNetworkData[] skills)
    {
        int count = 0;
        this.skills = new SkillData[skills.Length];
        
        for (int i = 0; i < skills.Length; i++)
        {
            SkillNetworkData currentSkill = skills[i];
            // 장착된 스킬은 장착
            if (currentSkill.isEquipped == "Y")
            {
                this.equippedSkills[count] = new SkillData(currentSkill);
                count++;
            }
            else
            {
                this.skills[i] = new SkillData(currentSkill);
            }
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

    public bool IsEquippedSkillReady()
    {
        return this.equippedSkills.Length == 4;
    }
}
