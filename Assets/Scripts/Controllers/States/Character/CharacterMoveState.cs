using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

// 캐릭터 움직임 상태
public class CharacterMoveState : MonoBehaviour, ICharacterState
{
        private CharacterController characterController;
        private Vector3 targetDirection;

        public void Enter(CharacterController characterController)
        {
                Debug.Log("Enter Move State");
                this.characterController = characterController;
                characterController.animator.SetBool(CharacterAnimatorParameters.IsMove, true);
                StartCoroutine(Move());
                // 로직 처리
                // 애니메이션도 처리
        }

        private IEnumerator Move()
        {
                WaitForEndOfFrame oneFrame = new WaitForEndOfFrame();
                while (true)
                {
                        targetDirection = new Vector3(-characterController.moveJoystick.input.x, 0,
                                -characterController.moveJoystick.input.y);
                        
                        if (targetDirection != Vector3.zero)
                        {
                                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                                characterController.transform.rotation = targetRotation;
                        }
                        
                        characterController.transform.position += targetDirection * characterController.moveSpeed * Time.deltaTime;
                        
                        yield return oneFrame;
                }
                
                // 트랙패드 방향으로 움직임 구현
        }
        
        public void Exit(CharacterController characterController)
        {
                Debug.Log("Exit Move State");
                characterController.animator.SetBool(CharacterAnimatorParameters.IsMove, false);
                StopAllCoroutines();
        }
}