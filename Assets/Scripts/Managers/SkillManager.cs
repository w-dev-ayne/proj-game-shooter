using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    public SkillDataController dataController;
    public SkillData[] skills;
    public SkillData[] equippedSkills;

    void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        DrawSkill();
    }

    public void GetSkills()
    {
        //dataController.GetUserSkills();
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
