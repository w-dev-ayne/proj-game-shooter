using UnityEngine;

public class MoveSkill : Skill
{
    public MoveSkill(SkillData data) : base(data)
    {
        
    }

    public override bool Action(CharacterController cc)
    {
        if (!base.Action(cc))
        {
            return false;
        }
        
        Vector3 targetPosition = cc.transform.position + (cc.transform.forward * amount);
        cc.transform.position = targetPosition;

        return true;
    }
}
