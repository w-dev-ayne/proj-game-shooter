using UnityEngine;

public class AnimatorController
{
    protected Animator animator;

    protected AnimatorController(Animator animator)
    {
        this.animator = animator;
    }

    protected float GetClipDurationByName(string clipName)
    {
        RuntimeAnimatorController controller = animator.runtimeAnimatorController;

        AnimationClip[] clips = controller.animationClips;

        foreach (AnimationClip clip in clips)
        {
            if (clip.name == clipName)
            {
                return clip.length;
            }
        }

        return 0.0f;
    }
}