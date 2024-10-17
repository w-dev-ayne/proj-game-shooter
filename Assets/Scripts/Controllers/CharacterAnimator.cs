using UnityEngine;

public class CharacterAnimator : AnimatorController
{
    public CharacterController cc;
    
    public void StartAttack()
    {
        animator.SetBool(CharacterAnimatorParameters.IsAttack, true);
    }
    
    public void Attack()
    {
        animator.SetTrigger(CharacterAnimatorParameters.Attack);
    }

    public void FinishAttack()
    {
        animator.SetBool(CharacterAnimatorParameters.IsAttack, false);
    }

    public void StartMove()
    {
        animator.speed = cc.moveSpeed / 2;
        animator.SetBool(CharacterAnimatorParameters.IsMove, true);
    }

    public void FinishMove()
    {
        animator.speed = 1;
        animator.SetBool(CharacterAnimatorParameters.IsMove, false);
    }
}

public static class CharacterAnimatorParameters
{
    public static int IsMove = Animator.StringToHash("IsMove");
    public static int Attack = Animator.StringToHash("Attack");
    public static int IsAttack = Animator.StringToHash("IsAttack");
}
