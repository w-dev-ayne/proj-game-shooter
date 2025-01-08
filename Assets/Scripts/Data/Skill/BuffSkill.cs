using System.Collections;
using UnityEngine;

public class BuffSkill : Skill
{
    public BuffSkill(SkillData data) : base(data)
    {
        
    }

    public override bool Action(CharacterController cc)
    {
        if (!base.Action(cc))
        {
            return false;
        }
        
        if (vfx != null)
        {
            vfxObject.gameObject.SetActive(true);
            vfxObject.Play();
        }

        UnLockOtherSkill();
        cc.SetAttack(amount);
        Managers.Stage.skillTimer.BuffTimer(duration, FinishBuff);
        return true;
    }

    private void FinishBuff(CharacterController cc)
    {
        cc.SetAttack(-amount);
        vfxObject.Stop();
        cc.onStatusChanged.Invoke(cc);
    }
}
