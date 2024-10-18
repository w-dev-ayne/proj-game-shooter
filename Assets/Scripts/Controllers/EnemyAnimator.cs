using UnityEngine;

public class EnemyAnimator : AnimatorController
{
    private EnemyController ec;

    public float hitDurataion = 0.0f;

    public EnemyAnimator(EnemyController ec, Animator animator) : base(animator)
    {
        this.ec = ec;
        hitDurataion = GetClipDurationByName("GetHit");
        Debug.Log(hitDurataion.ToString());
    }

    public void StartAttack()
    {
        animator.SetBool(EnemyAnimatorParameters.IsAttack, true);
    }

    public void Attack()
    {
        animator.SetTrigger(EnemyAnimatorParameters.Attack);
    }

    public void FinishAttack()
    {
        animator.SetBool(EnemyAnimatorParameters.IsAttack, false);
    }

    public void StartMove()
    {
        animator.SetBool(EnemyAnimatorParameters.IsMove, true);
    }

    public void FinishMove()
    {
        animator.SetBool(EnemyAnimatorParameters.IsMove, false);
    }

    public void Hit()
    {
        animator.SetTrigger(EnemyAnimatorParameters.Hit);
    }

    public void Die()
    {
        animator.SetTrigger(EnemyAnimatorParameters.Die);
    }

    public void Victory()
    {
        animator.SetTrigger(EnemyAnimatorParameters.Victory);
    }
}

public static class EnemyAnimatorParameters
{
    public static int IsMove = Animator.StringToHash("IsMove");
    public static int IsAttack = Animator.StringToHash("IsAttack");
    public static int Attack = Animator.StringToHash("Attack");
    public static int Hit = Animator.StringToHash("Hit");
    public static int Die = Animator.StringToHash("Die");
    public static int Victory = Animator.StringToHash("Victory");
}