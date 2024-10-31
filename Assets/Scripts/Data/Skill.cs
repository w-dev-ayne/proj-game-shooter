using UnityEngine;

public class Skill : ISkill
{
    protected Define.SkillType type;
    protected int amount;
    protected int cost;
    protected float range;
    protected float duration;
    protected float coolTime;

    public Sprite skillIcon;

    protected ParticleSystem vfx;
    
    public Skill(SkillData data)
    {
        type = data.type;
        cost = data.cost;
        amount = data.amount;
        range = data.range;
        duration = data.duration;
        coolTime = data.coolTime;
        vfx = data.vfx;
        skillIcon = data.skillIcon;
    }

    public virtual void Action(CharacterController cc)
    {
        
    }
}