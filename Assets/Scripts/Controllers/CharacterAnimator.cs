using UnityEngine;

public class CharacterAnimator : AnimatorController
{
    public void Attack()
    {
        float duration = GetClipDurationByName("Shoot");
    }
}

public static class CharacterAnimatorParameters
{
    public static int IsMove = Animator.StringToHash("IsMove");
    public static int Attack = Animator.StringToHash("Attack");
    public static int IsAttack = Animator.StringToHash("IsAttack");
}
