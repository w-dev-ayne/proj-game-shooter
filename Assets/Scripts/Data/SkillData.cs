using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

[System.Serializable]
public class SkillData
{
    public int id;
    public string name;
    public Define.SkillType type;
    public float amount;
    public float cost;
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
public class SkillUpgradeData
{
    public float amount;
    public float cost;
    public float range;
    public float duration;
    public float delay;
    public float cooltime;

    public void FetchData(SkillData data)
    {
        amount = data.amount;
        cost = data.cost;
        range = data.range;
        duration = data.duration;
        delay = data.delay;
        cooltime = data.coolTime;
    }
}

[System.Serializable]
public class SkillNetworkData : PostData
{
    public int id;
    public string name;
    public string type;
    public float amount;
    public float cost;
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

[System.Serializable]
public class ConfigurationNetworkData
{
    public string type;
    public float amount;
}

public class SkillUpgradeConfiguration
{
    public float amount;
    public float cost;
    public float range;
    public float duration;
    public float delay;
    public float cooltime;

    public void FetchData(ConfigurationNetworkData[] datas)
    {
        foreach (ConfigurationNetworkData data in datas)
        {
            switch (data.type)
            {
                case "AMOUNT":
                    amount = data.amount;
                    break;
                case "COST":
                    cost = data.amount;
                    break;
                case "RANGE":
                    range = data.amount;
                    break;
                case "DURATION":
                    duration = data.amount;
                    break;
                case "DELAY":
                    delay = data.amount;
                    break;
                case "COOLTIME":
                    cooltime = data.amount;
                    break;
            }
        }
    }

    public void Print()
    {
        Debug.Log($"Amount : {amount}");
        Debug.Log($"Cost : {cost}");
        Debug.Log($"Range : {range}");
        Debug.Log($"Duration : {duration}");
        Debug.Log($"Delay : {delay}");
        Debug.Log($"CoolTime : {cooltime}");
    }
}

[System.Serializable]
public class SkillUpgradeNetworkData : PostData
{
    public int skillId;
    public int amount;
    public int cost;
    public int range;
    public int duration;
    public int delay;
    public int cooltime;
}

[System.Serializable]
public class SkillEquipNetworkData : PostData
{
    public int equip;
    public int unEquip;

    public SkillEquipNetworkData(int e, int u)
    {
        equip = e;
        unEquip = u;
    }
}