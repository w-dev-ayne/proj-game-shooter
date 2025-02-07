using UnityEngine;
using UnityEngine.VFX;

public class HealSkill : Skill
{
    public HealSkill(SkillData data) : base(data)
    {
        
    }

    public override bool Action(CharacterController cc)
    {
        if (cc.hp == cc.maxHp)
        {
            Managers.Instruction.InstructionOn(InstructionDefine.MAX_HP);
            return false;
        }
            
        
        if (!base.Action(cc))
        {
            return false;
        }

        if (vfx != null)
        {
            vfxObject.gameObject.SetActive(true);
            vfxObject.Play();
        }
        
        base.PlaySound();

        if (duration == 0)
        {
            cc.Heal(amount);    
        }
        else
        {
            Managers.UI.FindPopup<UI_InGame>().StartBuffIcon(this);
            Managers.Stage.skillTimer.DotAction(duration, () =>
            {
                cc.Heal((float)amount / (float)duration);
            });
        }

        UnLockOtherSkill();
        return true;
    }
}
