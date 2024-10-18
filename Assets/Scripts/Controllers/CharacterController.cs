using UnityEditor.Timeline;
using UnityEngine;

public class CharacterController : MonoBehaviour, IDamageable
{
    private ICharacterState moveState;
    private ICharacterState rotateState;
    private ICharacterState attackState;
    
    private CharacterStateContext stateContext;
    
    public Joystick moveJoystick;
    public Joystick attackJoystick;

    public float attackSpeed = 4.0f;
    public ParticleSystem attackParticle;
    
    public float moveSpeed = 1.0f;
    public float rotateSpeed = 1.0f;
    public float bulletSpeed = 1.0f;
    
    public BulletPool bulletPool;
    
    public CharacterAnimator animatorController;


    private void Start()
    {
        // State Context 등록
        this.stateContext = new CharacterStateContext(this);
        animatorController = new CharacterAnimator(this, this.GetComponent<Animator>());
        
        // State 등록
        moveState = this.gameObject.AddComponent<CharacterMoveState>();
        attackState = this.gameObject.AddComponent<CharacterAttackState>();
        
        moveJoystick.onDrag = Move;
        
        attackJoystick.onDrag = Attack;
        attackJoystick.onEndDrag = AttackToMove;
    }

    private void Move()
    {
        stateContext.Transition(moveState);
    }

    private void Attack()
    {
        stateContext.Transition(attackState);
    }

    private void AttackToMove()
    {
        if (moveJoystick.isDragging)
        {
            this.Move();   
        }
    }

    public void TakeDamage(float damage)
    {
        
    }

    #region DebugMode

    public void OnClickAttackButton()
    {
        Attack();
    }

    #endregion
}