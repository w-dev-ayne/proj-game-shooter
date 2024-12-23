using Unity.VisualScripting;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    public SkillData[] skills;
    private SkillData[] equippedSkills;

    void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        GetSkills();
    }

    private void GetSkills()
    {
        Managers.Network.skillController.GetUserSkills();
    }

    public void FetchSkill(SkillNetworkData[] skills)
    {
        this.skills = new SkillData[skills.Length];
        
        for (int i = 0; i < skills.Length; i++)
        {
            this.skills[i] = new SkillData(skills[i]);
        }
    }

    public async void DrawSkill()
    {
        await Managers.Network.skillController.DrawSkill();
        Debug.Log("Skill Drew");
    }

    public void UpgradeSkill()
    {
        //dataController.UpgradeSkill();
    }
    
}
