using UnityEngine;

public class CoolTimeState : IState<Skill>
{
    public void Enter(Skill skill)
    {
        skill.coolTimeImage.gameObject.SetActive(true);
        StageManager.Instance.skillManager.CoolTimer(skill);
    }
    
    
    public void Exit(Skill skill)
    {
        
    }
}
