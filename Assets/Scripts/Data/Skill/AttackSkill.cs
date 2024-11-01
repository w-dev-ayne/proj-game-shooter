using System.Collections.Generic;
using UnityEngine;

public class AttackSkill : Skill
{
    public AttackSkill(SkillData data) : base(data)
    {
        
    }
    
    public override void Action(CharacterController cc)
    {
        base.Action(cc);
        
        Collider[] colliders = Physics.OverlapSphere(cc.transform.position, range);
        List<EnemyController> enemies = new List<EnemyController>();

        if (vfx != null)
        {
            GameObject.Instantiate(vfx, cc.transform.position, Quaternion.identity);
            vfx.Play();
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
    }
}