using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Skill : ISkill
{
    protected Define.SkillType type;
    protected int amount;
    protected int cost;
    protected float range;
    protected float duration;
    protected float coolTime;
    public bool isCoolTime;

    public Sprite skillIcon;

    protected ParticleSystem vfx;
    protected ParticleSystem vfxObject;

    public Image coolTimeImage;
    
    public Skill(SkillData data)
    {
        type = data.type;
        cost = data.cost;
        amount = data.amount;
        range = data.range;
        duration = data.duration;
        coolTime = data.coolTime;
        vfx = data.vfx;
        skillIcon = data.skillIcon;

        if (vfx != null)
        {
            vfxObject = GameObject.Instantiate(vfx) as ParticleSystem;
            vfxObject.transform.parent = StageManager.Instance.cc.transform;
            vfxObject.transform.localPosition = Vector3.zero;
            vfxObject.gameObject.SetActive(false);   
        }
    }

    public virtual void Action(CharacterController cc)
    {
        cc.mp -= cost;
        StageManager.Instance.skillManager.CoolTimer(this);
    }

    private IEnumerator CoolTimeRoutine()
    {
        yield break;
    }
}