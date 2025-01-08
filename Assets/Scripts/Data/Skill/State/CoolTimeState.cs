using UnityEngine;

public class CoolTimeState : IState<Skill>
{
    public void Enter(Skill skill)
    {
        Debug.Log("Start Cool Time");
        skill.coolTimeImage.gameObject.SetActive(true);
        Managers.Stage.skillTimer.CoolTimer(skill);
    }
    
    
    public void Exit(Skill skill)
    {
        
    }
}
