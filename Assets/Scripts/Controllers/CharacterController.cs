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

    public float maxHp { get; private set; }
    public float hp { get; private set; }
    public float maxMp { get; private set; }
    public float mp { get; private set; }
    public float attack { get; private set; }
    public float moveSpeed { get; private set; }
    public float rotateSpeed { get; private set; }
    public float bulletSpeed { get; private set; }
    public float attackSpeed { get; private set; }
    
    public BulletPool bulletPool;
    
    public CharacterAnimator animatorController;
    public Rigidbody rb;

    public UnityAction<CharacterController> onStatusChanged;

    public SkillData[] skillDatas;
    public Skill[] skills { get; private set; }
    public Transform skillRange;

    void Awake()
    {
        Init();
    }

    void Start()
    {
        
    }

    public void Init()
    {
        InitializeCharacterData();
        InitializeSkillData();
        InitializeStateData();
        
        moveJoystick.onDrag = Move;
        moveJoystick.onEndDrag = MoveToAttack;
        attackJoystick.onDrag = Attack;
        attackJoystick.onEndDrag = AttackToMove;
        rb = GetComponent<Rigidbody>();
        bulletPool.cc = this;
        onStatusChanged?.Invoke(this);
    }

    // 캐릭터 상태 정보 초기화
    private void InitializeStateData()
    {
        this.stateContext = new StateContext<CharacterController>(this);
        animatorController = new CharacterAnimator(this, this.GetComponent<Animator>());
        
        // State 등록
        idleState = this.gameObject.AddComponent<CharacterIdleState>();
        moveState = this.gameObject.AddComponent<CharacterMoveState>();
        attackState = this.gameObject.AddComponent<CharacterAttackState>();
    }

    // 캐릭터 기본 데이터 초기화
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

    // 캐릭터 스킬 생성 및 정보 초기화
    private void InitializeSkillData()
    {
        skills = new Skill[skillDatas.Length];

        for (int i = 0; i < skillDatas.Length; i++)
        {
            string typeName = $"{skillDatas[i].type.ToString()}Skill";
            Type skillType = Type.GetType(typeName);
            skills[i] = (Skill)Activator.CreateInstance(skillType, new object[] { skillDatas[i] });
        }
        // Managers.UI.FindPopup<UI_InGame>().SetSkillButtons(skills);
    }
 
    // 캐릭터 스킬 사용
    public void Skill(Skill skill)
    {
        if (skill.Action(this))
        {
            UseMp(skill.cost);
            onStatusChanged.Invoke(this);    
        }
    }

    // MP 사용 (부족 시 false return)
    private void UseMp(float amount)
    {
        mp -= amount;
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

    private void MoveToAttack()
    {
        if (attackJoystick.isDragging)
        {
            this.Attack();
        }
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        onStatusChanged.Invoke(this);
    }

    public void Heal(float amount)
    {
        hp += amount;
    }

    public void SetAttack(float amount)
    {
        attack += amount;
    }

    private void OnDestroy()
    {
        
    }

    #region DebugMode

    public void OnClickAttackButton()
    {
        Attack();
    }

    #endregion
}