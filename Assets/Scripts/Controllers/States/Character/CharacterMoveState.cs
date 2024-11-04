using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

// 캐릭터 움직임 상태
public class CharacterMoveState : Rotatable, IState<CharacterController>
{
        private CharacterController cc;
        private Vector3 targetDirection;
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
                        targetDirection = new Vector3(cc.moveJoystick.input.x, 0,
                                cc.moveJoystick.input.y);
                        
                        if (targetDirection != Vector3.zero)
                        {
                                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                                cc.transform.rotation = targetRotation;
                        }
                        
                        cc.rb.MovePosition(cc.transform.position + targetDirection * Time.deltaTime * cc.moveSpeed);
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