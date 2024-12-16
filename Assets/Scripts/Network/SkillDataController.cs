using UnityEngine;

public class SkillDataController : APILoader
{
    public SkillsData skillsData = new SkillsData();
    public SkillData skillData = new SkillData();
    
    public void GetUserSkills()
    {
        base.GetAPI($"{NetworkDefine.Host}/api", null, skillsData);
    }

    public void DrawSkill()
    {
        
    }

    public void UpgradeSkill()
    {
        
    }
}
