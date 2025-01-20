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

    // Attack Speed에 맞춰 Bullet Pool에서 꺼내서 발사
    private IEnumerator Attack()
    {
        Vector3 direction = Vector3.zero;
        WaitForSeconds attackSpeed = new WaitForSeconds(1.0f / cc.attackSpeed);
        WaitForEndOfFrame oneFrame = new WaitForEndOfFrame();

        // 애니메이션 전환시간 대기
        yield return new WaitForSeconds(0.2f);
        
        while (cc.attackJoystick.isDragging)
        {
            direction.x = cc.attackJoystick.input.x;
            direction.y = 0;
            direction.z = cc.attackJoystick.input.y;
            direction.Normalize();

            if (cc.attackJoystick.input.magnitude == 0 || Vector3.Angle(direction, cc.transform.forward) > 10f)
            {
                yield return oneFrame;
                continue;
            }
            
            this.cc.animatorController.Attack();
            cc.bulletPool.attackParticle.Play();
            Bullet bullet = cc.bulletPool.TakeFromPool() as Bullet;
            bullet.transform.position = cc.bulletPool.shootPositionTransform.position;
            bullet.Shoot(direction, cc.bulletSpeed,cc.attack);
            Managers.Sound.Play(Define.Sound.Effect, AudioDefine.CHARACTER_SHOOT_EFFECT);
            
            //bullet 발사 로직 구현
            yield return attackSpeed;
        }
        // LookAt 방향으로 공격 구현
        Exit(cc);
    }

    // 공격하는 방향으로 회전
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