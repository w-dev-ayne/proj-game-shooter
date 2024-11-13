using UnityEngine;

public class SkillVfxScale : MonoBehaviour
{
    [SerializeField] private float factor = 1;

    void Awake()
    {
        this.transform.localScale = this.transform.localScale / this.factor;
    }
}
