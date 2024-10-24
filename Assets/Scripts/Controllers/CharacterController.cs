using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour, IDamageable
{
    private IState<CharacterController> idleState;
    private IState<CharacterController> moveState;
    private IState<CharacterController> attackState;
    
    private StateContext<CharacterController> stateContext;

    public UnityEvent onDamage {get; set;}
    
    public Joystick moveJoystick;
    public Joystick attackJoystick;

    
    public ParticleSystem attackParticle;

    public CharacterData data;

    public float hp = 100f;
    public float attack;
    public float moveSpeed = 1.0f;
    public float rotateSpeed = 1.0f;
    public float bulletSpeed = 1.0f;
    public float attackSpeed = 4.0f;
    
    public BulletPool bulletPool;
    
    public CharacterAnimator animatorController;


    private void Start()
    {
        AdjustData();
        // State Context 등록
        this.stateContext = new StateContext<CharacterController>(this);
        animatorController = new CharacterAnimator(this, this.GetComponent<Animator>());
        
        // State 등록
        idleState = this.gameObject.AddComponent<CharacterIdleState>();
        moveState = this.gameObject.AddComponent<CharacterMoveState>();
        attackState = this.gameObject.AddComponent<CharacterAttackState>();
        
        moveJoystick.onDrag = Move;
        
        attackJoystick.onDrag = Attack;
        attackJoystick.onEndDrag = AttackToMove;

        bulletPool.cc = this;
    }

    private void AdjustData()
    {
        this.hp = data.hp;
        this.attack = data.attack;
        this.moveSpeed = data.moveSpeed;
        this.rotateSpeed = data.rotateSpeed;
        this.bulletSpeed = data.bulletSpeed;
        this.attackSpeed = data.attackSpeed;
    }

    public void Idle()
    {
        stateContext.Transition(idleState);
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