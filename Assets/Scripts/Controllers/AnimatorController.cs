using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    protected Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected float GetClipDurationByName(string clipName)
    {
        RuntimeAnimatorController controller = animator.runtimeAnimatorController;

        AnimationClip[] clips = controller.animationClips;

        foreach (AnimationClip clip in clips)
        {
            if (clip.name == clipName)
            {
                Debug.Log($"Found clip {clip.name} Duration {clip.length}");
                return clip.length;
            }
        }

        return 0.0f;
    }
    
    public virtual void UpdateAnimator()
    {
        if (animator == null)
            return;
    }

    void Update()
    {
        UpdateAnimator();
    }
}