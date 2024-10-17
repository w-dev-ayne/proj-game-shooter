using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterRotateState : MonoBehaviour, ICharacterState
{
        private CharacterController cc;
        private Vector3 targetDirection;

        public void Enter(CharacterController cc)
        {
                Debug.Log("Enter Rotate State");
                this.cc = cc;
                StartCoroutine(Rotate());
        }

        private IEnumerator Rotate()
        {
         
                WaitForEndOfFrame oneFrame = new WaitForEndOfFrame();
                
                while (cc.attackJoystick.isDragging)
                {
                        targetDirection = new Vector3(-cc.attackJoystick.input.x, 0,
                                -cc.attackJoystick.input.y);
                        // 목표 회전 각도
                        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                        // 현재 회전에서 목표 회전으로 회전
                        cc.transform.rotation = Quaternion.RotateTowards(
                                cc.transform.rotation,
                                targetRotation,
                                cc.rotateSpeed * Time.deltaTime * 180 // 가변적인 rotateSpeed 적용
                        );       
                        yield return oneFrame;
                } 
                Exit(cc);
        }

        public void Exit(CharacterController cc)
        {
                Debug.Log("Exit Rotate State");
                StopAllCoroutines();
        }
}