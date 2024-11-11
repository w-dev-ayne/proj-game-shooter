using UnityEngine;

public class CoolTimeState : IState<Skill>
{
    public void Enter(Skill skill)
    {
        skill.coolTimeImage.gameObject.SetActive(true);
        Managers.Stage.skillTimer.CoolTimer(skill);
    }
    
    
    public void Exit(Skill skill)
    {
        
    }
}
