using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    public SkillDataController dataController;
    public SkillData[] skills;
    public SkillData[] equippedSkills;

    void Awake()
    {
        base.Awake();
        dataController = new SkillDataController();
    }

    public void GetSkills()
    {
        dataController.GetUserSkills();
    }

    public void DrawSkill()
    {
        dataController.DrawSkill();
    }

    public void UpgradeSkill()
    {
        dataController.UpgradeSkill();
    }
    
}
