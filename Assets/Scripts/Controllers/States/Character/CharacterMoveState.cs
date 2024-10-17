using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

// 캐릭터 움직임 상태
public class CharacterMoveState : Rotatable, ICharacterState
{
        private CharacterController cc;
        private Vector3 targetDirection;

        public void Enter(CharacterController cc)
        {
                Debug.Log("Enter Move State");
                this.cc = cc;
                this.cc.animatorController.StartMove();
                StartCoroutine(Move());
                StartCoroutine(Rotate(cc, cc.moveJoystick));
                // 로직 처리
                // 애니메이션도 처리
        }

        private IEnumerator Move()
        {
                WaitForEndOfFrame oneFrame = new WaitForEndOfFrame();
                while (true)
                {
                        targetDirection = new Vector3(-cc.moveJoystick.input.x, 0,
                                -cc.moveJoystick.input.y);
                        
                        if (targetDirection != Vector3.zero)
                        {
                                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                                cc.transform.rotation = targetRotation;
                        }
                        
                        cc.transform.position += targetDirection * cc.moveSpeed * Time.deltaTime;
                        
                        yield return oneFrame;
                }
                // 트랙패드 방향으로 움직임 구현
        }
        
        public void Exit(CharacterController cc)
        {
                Debug.Log("Exit Move State");
                this.cc.animatorController.FinishMove();
                StopAllCoroutines();
        }
}