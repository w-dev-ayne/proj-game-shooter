using UnityEngine;
using UnityEngine.VFX;

public class HealSkill : Skill
{
    public HealSkill(SkillData data) : base(data)
    {
        
    }

    public override bool Action(CharacterController cc)
    {
        if(cc.hp == cc.maxHp)
            return false;
        
        if (!base.Action(cc))
        {
            return false;
        }

        if (vfx != null)
        {
            vfxObject.gameObject.SetActive(true);
            vfxObject.Play();
        }
        
        cc.Heal(amount);
        return true;
    }
}
