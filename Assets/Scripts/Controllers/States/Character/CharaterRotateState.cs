using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterRotateState : MonoBehaviour, ICharacterState
{
        private CharacterController characterController;
        private Vector2 direction;

        public void Enter(CharacterController characterController)
        {
                this.direction = characterController.rotateDirection;
                StartCoroutine(Rotate());
        }

        private IEnumerator Rotate()
        {
                WaitForEndOfFrame oneFrame = new WaitForEndOfFrame();
                Vector3 currentDirection = characterController.transform.up;
                Vector3 target3D = new Vector3(direction.x, 0, direction.y);
                // 목표 회전 각도
                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, target3D);
                
                while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
                {
                        // 현재 회전에서 목표 회전으로 회전
                        characterController.transform.rotation = Quaternion.RotateTowards(
                                characterController.transform.rotation,
                                targetRotation,
                                characterController.rotateSpeed * Time.deltaTime * 360f // 가변적인 rotateSpeed 적용
                        );
                        yield return oneFrame;
                } 
                Exit(characterController);
        }

        public void Exit(CharacterController characterController)
        {
                StopAllCoroutines();
        }
}