using System.Collections;
using UnityEngine;

public class CharacterAttackState : Rotatable, IState<CharacterController>
{
    private CharacterController cc;

    public void Enter(CharacterController cc)
    {
        this.cc = cc;
        
        this.cc.animatorController.StartAttack();
        StartCoroutine(Attack());
        StartCoroutine(Rotate(cc, cc.attackJoystick));
    }

    public IEnumerator Attack()
    {
        WaitForSeconds attackSpeed = new WaitForSeconds(1.0f / cc.attackSpeed);
        while (cc.attackJoystick.isDragging)
        {
            this.cc.animatorController.Attack();
            cc.attackParticle.Play();
            Bullet bullet = cc.bulletPool.TakeFromPool() as Bullet;
            bullet.transform.position = cc.bulletPool.shootPositionTransform.position;
            Vector3 direction = new Vector3(cc.attackJoystick.input.x, 0, cc.attackJoystick.input.y).normalized;
            bullet.Shoot(direction);
            
            //bullet 발사 로직 구현
            yield return attackSpeed;
        }
        // LookAt 방향으로 공격 구현
        Exit(cc);
    }
    
    public void Exit(CharacterController cc)
    {
        this.cc.animatorController.FinishAttack();
        
        StopAllCoroutines();
    }
}