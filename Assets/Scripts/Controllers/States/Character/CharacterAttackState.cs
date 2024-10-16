using System.Collections;
using UnityEngine;

public class CharacterAttackState : MonoBehaviour, ICharacterState
{
    private CharacterController characterController;

    public void Enter(CharacterController characterController)
    {
        this.characterController = characterController;
    }

    public IEnumerator Attack()
    {
        WaitForEndOfFrame oneFrame = new WaitForEndOfFrame();
        while (true)
        {
            Bullet bullet = characterController.bulletPool.TakeFromPool() as Bullet;
            //bullet 발사 로직 구현
            yield return oneFrame;
        }
        // LookAt 방향으로 공격 구현
        yield return null;
    }
    
    public void Exit(CharacterController characterController)
    {
        StopAllCoroutines();
    }
}