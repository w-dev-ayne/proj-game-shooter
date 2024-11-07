using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttackState : MonoBehaviour, IState<EnemyController>
{
    private EnemyController ec;
    
    public void Enter(EnemyController ec)
    {
        this.ec = ec;
        this.ec.animatorController.StartAttack();

        StartCoroutine(Attack());
    }
    
    public IEnumerator Attack()
    {
        WaitForSeconds attackSpeed = new WaitForSeconds(1.0f / ec.attackSpeed);
        while (ec.attackCondition)
        {
            this.ec.animatorController.Attack();
            // cc.attackParticle.Play();
            
            //bullet 발사 로직 구현
            yield return attackSpeed;
        }
        // LookAt 방향으로 공격 구현
        ec.Move();
    }

    public void Exit(EnemyController ec)
    {
        ec.animatorController.FinishAttack();
        StopAllCoroutines();
    }
}
