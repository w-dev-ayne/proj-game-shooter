using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/SkillData")]
[System.Serializable]
public class SkillData : ScriptableObject
{
    public int id;
    public string name;
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
    public bool isEquipped;
}

[System.Serializable]
public class SkillNetworkData
{
    public int id;
    public string name;
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
    public bool isEquipped;
}