using System;
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

    public float maxHp;
    public float hp = 100f;
    public float maxMp;
    public float mp;
    public float attack;
    public float moveSpeed = 1.0f;
    public float rotateSpeed = 1.0f;
    public float bulletSpeed = 1.0f;
    public float attackSpeed = 4.0f;
    
    public BulletPool bulletPool;
    
    public CharacterAnimator animatorController;
    public Rigidbody rb;

    public UnityAction<CharacterController> onStatusChanged;

    public SkillData[] skillDatas;
    private Skill[] skills;
    
    private void Start()
    {
        InitializeCharacterData();
        InitializeSkillData();
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
        
        rb = GetComponent<Rigidbody>();

        bulletPool.cc = this;
        
        onStatusChanged.Invoke(this);
    }

    private void InitializeCharacterData()
    {
        this.maxHp = data.hp;
        this.hp = data.hp;
        this.maxMp = data.mp;
        this.mp = data.mp;
        this.attack = data.attack;
        this.moveSpeed = data.moveSpeed;
        this.rotateSpeed = data.rotateSpeed;
        this.bulletSpeed = data.bulletSpeed;
        this.attackSpeed = data.attackSpeed;
    }

    private void InitializeSkillData()
    {
        skills = new Skill[skillDatas.Length];

        for (int i = 0; i < skillDatas.Length; i++)
        {
            string typeName = $"{skillDatas[i].type.ToString()}Skill";
            Type skillType = Type.GetType(typeName);
            skills[i] = (Skill)Activator.CreateInstance(skillType, new object[] { skillDatas[i] });
        }
    }

    public void Skill(int index)
    {
        // MP 처리
        skills[index].Action(this);
        onStatusChanged.Invoke(this);
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
        onStatusChanged.Invoke(this);
    }

    #region DebugMode

    public void OnClickAttackButton()
    {
        Attack();
    }

    #endregion
}