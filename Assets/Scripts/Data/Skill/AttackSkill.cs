using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSkill : Skill
{
    private Vector3 affectPosition;
    
    public AttackSkill(SkillData data) : base(data)
    {
        
    }
    
    public override bool Action(CharacterController cc)
    {
        if (!base.Action(cc))
        {
            return false;
        }
        
        // VFX 실행
        if (vfx != null && !vfxOnDelay)
        {
            PlayVFX(cc);
        }
        
        // Delay 종료후 Hit 판정
        Managers.Stage.skillTimer.DelayTimer(delay, range, () =>
        {
            if (vfxOnDelay)
            {
                PlayVFX(cc);
            }

            // 기준 위치 지정
            affectPosition = cc.transform.position;
            
            // 즉발 스킬
            if (duration == 0)
            {
                HitDamage(cc, amount);   
            }
            // 도트 스킬
            else
            {
                Managers.Stage.skillTimer.DotAction(duration, () =>
                {
                    HitDamage(cc, (float)amount / (float)duration);
                });
            }
            UnLockOtherSkill();
        });

        return true;  
    }

    private void HitDamage(CharacterController cc, float amount)
    {
        base.PlaySound();
        
        Collider[] colliders = Physics.OverlapSphere(affectPosition, range);
        List<EnemyController> enemies = new List<EnemyController>();
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Enemy"))
            {
                enemies.Add(col.GetComponent<EnemyController>());
            }
        }

        foreach (EnemyController enemy in enemies)
        {
            enemy.TakeDamage(amount);
        }
    }

    private void PlayVFX(CharacterController cc)
    {
        if (vfxOnDelay)
        {
            vfxObject.transform.SetParent(null);
            vfxObject.transform.localPosition = cc.transform.position;    
        }
        else
        {
            vfxObject.transform.SetParent(cc.transform);
            vfxObject.transform.localPosition = Vector3.zero;
        }
        vfxObject.gameObject.SetActive(true);
        vfxObject.Play();
    }
}