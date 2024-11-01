using UnityEngine;
using UnityEngine.VFX;

public class HealSkill : Skill
{
    public HealSkill(SkillData data) : base(data)
    {
        
    }

    public override void Action(CharacterController cc)
    {
        base.Action(cc);

        if (vfx != null)
        {
            vfxObject.gameObject.SetActive(true);
            vfxObject.Play();
        }
        
        cc.hp += amount;
    }
}
