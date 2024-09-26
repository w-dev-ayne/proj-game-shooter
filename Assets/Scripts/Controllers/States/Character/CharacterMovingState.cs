using Unity.VisualScripting;
using UnityEngine;

// 캐릭터 움직임 상태
public class CharacterMovingState : MonoBehaviour, ICharacterState
{
        private CharacterController characterController;

        public void Handle(CharacterController characterController)
        { 
                // 로직 처리
                // 애니메이션도 처리
        }
}