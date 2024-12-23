using System;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class SkillData
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

    public SkillData()
    {
        
    }
    
    public SkillData(SkillNetworkData data)
    {
        FetchData(data);
    }

    public void FetchData(SkillNetworkData data)
    {
        id = data.id;
        name = data.name;
        type = (Define.SkillType)Enum.Parse(typeof(Define.SkillType), data.type);
        amount = data.amount;
        cost = data.cost;
        range = data.range;
        duration = data.duration;
        delay = data.delay;
        vfxOnDelay = data.vfxOnDelay == "Y";
        coolTime = data.coolTime;
        vfx = Managers.Resource.Load<ParticleSystem>($"VFX/{data.vfx}");
        skillIcon = null;
        description = data.description;
        isEquipped = data.isEquipped == "Y";
    }
}

[System.Serializable]
public class SkillNetworkData : PostData
{
    public int id;
    public string name;
    public string type;
    public int amount;
    public int cost;
    public float range;
    public float duration;
    public float delay;
    public string vfxOnDelay;
    public float coolTime;
    public string vfx;
    public string skillIcon;
    public string description;
    public string isEquipped;
    
    public void FetchData(SkillData data)
    {
        id = data.id;
        name = data.name;
        type = data.type.ToString();
        amount = data.amount;
        cost = data.cost;
        range = data.range;
        duration = data.duration;
        delay = data.delay;
        vfxOnDelay = data.vfxOnDelay ? "Y" : "N";
        coolTime = data.coolTime;
        vfx = data.vfx?.ToString();
        skillIcon = null;
        description = data.description;
        isEquipped = data.isEquipped ? "Y" : "N";
    }
}