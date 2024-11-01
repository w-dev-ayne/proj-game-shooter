using System.Collections;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class BuffSkill : Skill
{
    public BuffSkill(SkillData data) : base(data)
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

        cc.attack += amount;
        StageManager.Instance.skillTimer.BuffTimer(duration, FinishBuff);
    }

    private void FinishBuff(CharacterController cc)
    {
        cc.attack -= amount;
        vfxObject.Stop();
        cc.onStatusChanged.Invoke(cc);
    }
}
