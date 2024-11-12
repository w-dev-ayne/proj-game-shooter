using System.Collections.Generic;
using UnityEngine;

public class AttackSkill : Skill
{
    public AttackSkill(SkillData data) : base(data)
    {
        
    }
    
    public override bool Action(CharacterController cc)
    {
        if (!base.Action(cc))
        {
            return false;
        }
        
        Collider[] colliders = Physics.OverlapSphere(cc.transform.position, range);
        List<EnemyController> enemies = new List<EnemyController>();

        cc.skillRange.localScale = Vector3.one * (range * 2);
        cc.skillRange.gameObject.SetActive(true);

        if (vfx != null)
        {
            vfxObject.gameObject.SetActive(true);
            vfxObject.Play();
        }

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

        return true;  
    }
}