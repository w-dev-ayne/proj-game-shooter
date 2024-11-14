using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/SkillData")]
public class SkillData : ScriptableObject
{
    public Define.SkillType type;
    
    public int amount;
    public int cost;
    public float range;
    public float duration;
    public float delay;
    public bool vfxOnDelay = false;
    public float coolTime;
    public ParticleSystem vfx;
    public Sprite skillIcon;
    public string description;
}
