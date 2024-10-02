using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterRotateState : MonoBehaviour, ICharacterState
{
        private CharacterController characterController;
        private Vector3 targetDirection;

        public void Enter(CharacterController characterController)
        {
                Debug.Log("Enter Rotate State");
                this.characterController = characterController;
                StartCoroutine(Rotate());
        }

        private IEnumerator Rotate()
        {
         
                WaitForEndOfFrame oneFrame = new WaitForEndOfFrame();
                
                while (true)
                {
                        targetDirection = new Vector3(-targetDirection.x, 0, -targetDirection.y);
                        // 목표 회전 각도
                        if (targetDirection != Vector3.zero)
                        {
                                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                                // 현재 회전에서 목표 회전으로 회전
                                characterController.transform.rotation = Quaternion.RotateTowards(
                                        characterController.transform.rotation,
                                        targetRotation,
                                        characterController.rotateSpeed * Time.deltaTime * 180 // 가변적인 rotateSpeed 적용
                                );       
                        }
                        yield return oneFrame;
                } 
                Exit(characterController);
        }

        public void Exit(CharacterController characterController)
        {
                Debug.Log("Exit Rotate State");
                StopAllCoroutines();
        }
}