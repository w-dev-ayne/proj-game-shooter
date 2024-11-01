using UnityEngine;

public class MoveSkill : Skill
{
    public MoveSkill(SkillData data) : base(data)
    {
        
    }

    public override void Action(CharacterController cc)
    {
        base.Action(cc);
        
        Vector3 targetPosition = cc.transform.position + (cc.transform.forward * amount);
        cc.transform.position = targetPosition;
    }
}
