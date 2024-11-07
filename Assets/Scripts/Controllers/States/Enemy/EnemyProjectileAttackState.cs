using System.Collections;
using UnityEngine;

public class EnemyProjectileAttackState : MonoBehaviour, IState<EnemyController>
{
    private EnemyController ec;
    
    public void Enter(EnemyController ec)
    {
        this.ec = ec;
        StartCoroutine(Attack());
    }

    
    private IEnumerator Attack()
    {
        WaitForSeconds attackSpeed = new WaitForSeconds(1.0f / ec.attackSpeed);

        while (ec.attackCondition)
        {
            this.ec.animatorController.Attack();

            EnemyBullet bullet = ec.bulletPool.TakeFromPool() as EnemyBullet;
            Vector3 target = ec.cc.transform.position;
            bullet.Shoot(target, ec.bulletSpeed, ec.attack);
            yield return attackSpeed;
        }
        
        ec.Move();
    }

    public void Exit(EnemyController ec)
    {
        ec.animatorController.FinishAttack();
        StopAllCoroutines();
    }
}
