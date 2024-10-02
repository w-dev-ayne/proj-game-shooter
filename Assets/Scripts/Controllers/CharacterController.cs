using UnityEditor.Timeline;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private ICharacterState moveState;
    private ICharacterState rotateState;
    private ICharacterState attackState;
    
    private CharacterStateContext stateContext;
    
    public Joystick moveJoystick;
    public Joystick attackJoystick;

    public float moveSpeed = 1.0f;
    public float rotateSpeed = 1.0f;


    private void Start()
    {
        // State Context 등록
        this.stateContext = new CharacterStateContext(this);

        moveJoystick.onDrag = Move;
        attackJoystick.onDrag = Attack;
        
        // State 등록
        moveState = this.gameObject.AddComponent<CharacterMoveState>();
        rotateState = this.gameObject.AddComponent<CharaterRotateState>();
        attackState = this.gameObject.AddComponent<CharacterAttackState>();
    }

    private void Move()
    {
        stateContext.Transition(moveState);
        //stateContext.Overlay(rotateState);
    }

    private void Attack()
    {
        stateContext.Transition(attackState);
        stateContext.Overlay(rotateState);
    }

    public void GetDamage(float amount)
    {
        
    }
}