using UnityEngine;

public class OperateState : IState<Skill>
{
    public void Enter(Skill skill)
    {
        skill.CoolTime();
    }
    
    public void Exit(Skill skill)
    {
        
    }
}
