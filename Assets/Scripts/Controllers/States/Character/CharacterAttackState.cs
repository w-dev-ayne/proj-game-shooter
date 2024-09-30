using System.Collections;
using UnityEngine;

public class CharacterAttackState : MonoBehaviour, ICharacterState
{
    private CharacterController characterController;

    public void Enter(CharacterController characterController)
    {
                
    }

    public IEnumerator Attack()
    {
        // LookAt 방향으로 공격 구현
        yield return null;
    }
    
    public void Exit(CharacterController characterController)
    {
        StopAllCoroutines();
    }
}