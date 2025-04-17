using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

// 캐릭터 움직임 상태
public class CharacterMoveState : Rotatable, IState<CharacterController>
{
        private CharacterController cc;
        private Vector3 targetDirection = Vector3.zero;
        private bool isMoving = false;

        public void Enter(CharacterController cc)
        {
                this.cc = cc;
                this.cc.animatorController.StartMove();
                
                isMoving = true;
        }

        void FixedUpdate()
        {
                if (isMoving && cc.moveJoystick.isDragging)
                {
                        targetDirection.x = cc.moveJoystick.input.x;
                        targetDirection.y = 0;
                        targetDirection.z = cc.moveJoystick.input.y;
                        targetDirection.Normalize();
                        
                        if (targetDirection != Vector3.zero)
                        {
                                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                                cc.transform.rotation = targetRotation;
                        }
                        
                        cc.rb.MovePosition(cc.transform.position + targetDirection * Time.deltaTime * cc.moveSpeed * FactorDefine.MOVE_SPEED);
                        base.Rotate(cc);
                }
                else if (isMoving && !cc.moveJoystick.isDragging)
                {
                        cc.Idle();
                }
        }
        
        public void Exit(CharacterController cc)
        {
                Debug.Log("Exit Move State");
                this.cc.animatorController.FinishMove();
                isMoving = false;
        }
}