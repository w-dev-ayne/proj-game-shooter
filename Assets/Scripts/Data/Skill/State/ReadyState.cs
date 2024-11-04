using UnityEngine;

public class ReadyState : IState<Skill>
{
    public void Enter(Skill skill)
    {
        if(skill.coolTimeImage != null)
            skill.coolTimeImage.gameObject.SetActive(false);
    }
    
    public void Exit(Skill skill)
    {
        
    }
}
