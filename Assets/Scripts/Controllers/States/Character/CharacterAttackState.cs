using System.Collections;
using UnityEngine;

public class CharacterAttackState : Rotatable, IState<CharacterController>
{
    private CharacterController cc;
    private bool isAttacking = false;
    
    public void Enter(CharacterController cc)
    {
        this.cc = cc;
        
        this.cc.animatorController.StartAttack();
        isAttacking = true;
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        WaitForSeconds attackSpeed = new WaitForSeconds(1.0f / cc.attackSpeed);
        while (cc.attackJoystick.isDragging)
        {
            yield return attackSpeed;
            
            Vector3 direction = new Vector3(cc.attackJoystick.input.x, 0, cc.attackJoystick.input.y).normalized;
            if (direction.x == 0.0f & direction.y == 0.0f)
            {
                yield return attackSpeed;
                continue;
            }
            
            this.cc.animatorController.Attack();
            cc.attackParticle.Play();
            Bullet bullet = cc.bulletPool.TakeFromPool() as Bullet;
            bullet.transform.position = cc.bulletPool.shootPositionTransform.position;
            bullet.Shoot(direction);
            //bullet 발사 로직 구현
        }
        // LookAt 방향으로 공격 구현
        Exit(cc);
    }

    void FixedUpdate()
    {
        if (isAttacking && cc.attackJoystick.isDragging)
        {
            base.Rotate(cc);
        }
    }
    
    public void Exit(CharacterController cc)
    {
        this.cc.animatorController.FinishAttack();
        StopAllCoroutines();
        isAttacking = false;
    }
}