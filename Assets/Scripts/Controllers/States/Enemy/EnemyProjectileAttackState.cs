using System.Collections;
using UnityEngine;

public class EnemyProjectileAttackState : MonoBehaviour, IState<EnemyController>
{
    private EnemyController ec;
    private bool isAttack = false;
    
    public void Enter(EnemyController ec)
    {
        this.ec = ec;
        this.ec.animatorController.StartAttack();
        isAttack = true;
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

    void FixedUpdate()
    {
        if(isAttack)
            this.transform.LookAt(ec.cc.transform);
    }
    

    public void Exit(EnemyController ec)
    {
        isAttack = false;
        ec.animatorController.FinishAttack();
        StopAllCoroutines();
    }
}
