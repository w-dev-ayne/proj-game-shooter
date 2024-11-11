using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour, IDamageable
{
    public Define.EnemyAttackType attackType;
    public ObjectPool bulletPool;
    
    private StateContext<EnemyController> stateContext;

    private IState<EnemyController> moveState;
    private IState<EnemyController> attackState;
    private IState<EnemyController> hitState;
    private IState<EnemyController> dieState;
    private IState<EnemyController> victoryState;
    
    public EnemyAnimator animatorController;

    public CharacterController cc;

    private const float ATTACK_RANGE = 10f;
    public bool attackCondition = false;


    public EnemyData data;
    public float attackSpeed = 2.0f;
    public float hp;
    public float attack;
    public float moveSpeed;
    public float attackRange;
    public float bulletSpeed;

    public float currentHp;
    
    public Image hpBar;
    
    public UnityEvent onDamage { get; set; }

    public void Initialize()
    {
        AdjustData();
        
        this.stateContext = new StateContext<EnemyController>(this);
        animatorController = new EnemyAnimator(this, this.GetComponent<Animator>());

        moveState = gameObject.AddComponent<EnemyMoveState>();

        switch (attackType)
        {
            case Define.EnemyAttackType.Melee:
                attackState = gameObject.AddComponent<EnemyMeleeAttackState>();
                break;
            case Define.EnemyAttackType.Projectile:
                attackState = gameObject.AddComponent<EnemyProjectileAttackState>();
                break;
        }
        
        hitState = gameObject.AddComponent<EnemyHitState>();
        dieState = gameObject.AddComponent<EnemyDieState>();
        victoryState = gameObject.AddComponent<EnemyVictoryState>();
        
        // 예외 처리 필요
        cc = FindObjectOfType<CharacterController>();

        attackCondition = Vector3.Distance(transform.position, cc.transform.position) <= attackRange;
        
        Move();
    }

    private void AdjustData()
    {
        this.hp = data.hp;
        this.attack = data.attack;
        this.moveSpeed = data.moveSpeed;
        this.attackSpeed = data.attackSpeed;
        this.attackRange = data.attackRange;
        this.bulletSpeed = data.bulletSpeed;
        currentHp = hp;
    }

    void Update()
    {
        attackCondition = Vector3.Distance(transform.position, cc.transform.position) <= attackRange; 
    }

    public void Move()
    {
        stateContext.Transition(moveState);
    }

    public void Attack()
    {
        stateContext.Transition(attackState);
    }

    private void Victory()
    {
        stateContext.Transition(victoryState);
    }

    private void Die()
    {
        stateContext.Transition(dieState);
        Managers.Stage.UpdateCurrentLevelKill();
    }
    
    public void TakeDamage(float damage)
    {
        if (currentHp <= 0)
            return;
        
        currentHp -= damage;
        hpBar.fillAmount = currentHp / hp;

        if (currentHp <= 0)
        {
            Die();
            return;
        }
        stateContext.Transition(hitState);
    }
}