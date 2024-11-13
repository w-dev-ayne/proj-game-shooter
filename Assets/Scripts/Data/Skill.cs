using System.Collections;
using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.UI;

public class Skill : ISkill
{
    protected Define.SkillType type;
    protected int amount;
    public int cost { get; }
    protected float range;
    protected float duration;
    public float coolTime { get; }

    public Sprite skillIcon;

    protected ParticleSystem vfx;
    protected ParticleSystem vfxObject;

    public Image coolTimeImage;
    
    private StateContext<Skill> stateContext;
    private IState<Skill> readyState;
    private IState<Skill> operateState;
    private IState<Skill> coolTimeState;
    
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
        
        stateContext = new StateContext<Skill>(this);
        readyState = new ReadyState();
        operateState = new OperateState();
        coolTimeState = new CoolTimeState();
        
        stateContext.Transition(readyState);
    }

    public void Ready()
    {
        stateContext.Transition(readyState);
    }

    public virtual bool Action(CharacterController cc)
    {
        if (stateContext.CurrentState == coolTimeState)
        {
            Debug.Log("Skill Is In Cool Time");
            return false;
        }

        if (cc.mp < amount)
        {
            Debug.Log("Low MP");
            return false;
        }
        
        // 스킬 VFX 오브젝트 생성 및 초기화
        if (vfx != null && vfxObject == null)
        {
            vfxObject = GameObject.Instantiate(vfx) as ParticleSystem;
            vfxObject.transform.parent = Managers.Stage.cc.transform;
            vfxObject.transform.localPosition = Vector3.zero;
            vfxObject.gameObject.SetActive(false);   
        }

        
        stateContext.Transition(operateState);
        return true;
    }

    public void CoolTime()
    {
        stateContext.Transition(coolTimeState);
    }

    private IEnumerator CoolTimeRoutine()
    {
        yield break;
    }
}