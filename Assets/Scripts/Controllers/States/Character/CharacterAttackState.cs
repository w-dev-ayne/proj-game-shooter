using System.Collections;
using UnityEngine;

public class CharacterAttackState : MonoBehaviour, ICharacterState
{
    private CharacterController characterController;

    public void Enter(CharacterController characterController)
    {
        this.characterController = characterController;
        
        this.characterController.animatorController.Attack();
        characterController.animator.SetBool(CharacterAnimatorParameters.IsAttack, true);
        StartCoroutine(Attack());
    }

    public IEnumerator Attack()
    {
        WaitForSeconds attackSpeed = new WaitForSeconds(1.0f / characterController.attackSpeed);
        while (characterController.attackJoystick.isDragging)
        {
            characterController.animator.SetTrigger(CharacterAnimatorParameters.Attack);
            characterController.attackParticle.Play();
            // Bullet bullet = characterController.bulletPool.TakeFromPool() as Bullet;
            //bullet 발사 로직 구현
            yield return attackSpeed;
        }
        // LookAt 방향으로 공격 구현
        Exit(characterController);
    }
    
    public void Exit(CharacterController characterController)
    {
        characterController.animator.SetBool(CharacterAnimatorParameters.IsAttack, false);
        StopAllCoroutines();
    }
}