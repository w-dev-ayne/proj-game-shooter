using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

// 캐릭터 움직임 상태
public class CharacterMoveState : MonoBehaviour, ICharacterState
{
        private CharacterController characterController;

        public void Enter(CharacterController characterController)
        { 
                // 로직 처리
                // 애니메이션도 처리
        }

        public IEnumerator Move()
        {
                // 트랙패드 방향으로 움직임 구현
                yield return null;
        }
        
        public void Exit(CharacterController characterController)
        {
                StopAllCoroutines();
        }
}