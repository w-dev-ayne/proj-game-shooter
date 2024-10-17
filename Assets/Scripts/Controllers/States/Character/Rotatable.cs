using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatable : MonoBehaviour
{
        private Vector3 targetDirection;

        protected IEnumerator Rotate(CharacterController cc, Joystick joyStick)
        {
                WaitForEndOfFrame oneFrame = new WaitForEndOfFrame();
                
                while (joyStick.isDragging)
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
        }
}