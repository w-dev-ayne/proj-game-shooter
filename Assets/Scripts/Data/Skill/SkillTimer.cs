using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillTimer : MonoBehaviour
{
    public List<bool> completes = new List<bool>();
    public bool skillRunning = false;
    
    public void CoolTimer(Skill skill)
    {
        StartCoroutine(CoolTimerRoutine(skill));
    }

    private IEnumerator CoolTimerRoutine(Skill skill)
    {
        WaitForEndOfFrame oneFrame = new WaitForEndOfFrame();
        float timer = skill.coolTime;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            skill.coolTimeImage.fillAmount = timer / skill.coolTime;
            yield return oneFrame;
        }
        
        skill.Ready();
    }
    
    public void BuffTimer(float duration, UnityAction<CharacterController> onComplete)
    {
        Debug.Log("Start Duration");
        StartCoroutine(BuffTimerRoutine(duration, onComplete));
    }
    
    private IEnumerator BuffTimerRoutine(float duration, UnityAction<CharacterController> onComplete)
    {
        yield return new WaitForSeconds(duration);
        Debug.Log("Finish Duration");
        onComplete.Invoke(Managers.Stage.cc);
    }

    public void DelayTimer(float duration, float range, UnityAction callback)
    {
        StartCoroutine(DelayTimerRoutine(duration, range, callback));
    }
    
    private IEnumerator DelayTimerRoutine(float duration, float range, UnityAction callback)
    {
        WaitForEndOfFrame oneFrame = new WaitForEndOfFrame();
        float timer = 0;
        Transform rangeObj = Managers.Stage.cc.skillRange;
        Transform rangeParent = Managers.Stage.cc.skillRangeParent;
        
        rangeParent.localScale = Vector3.one * (2 * range);
        rangeObj.localScale = Vector3.zero;
        rangeParent.gameObject.SetActive(true);

        while (timer < duration)
        {
            timer += Time.deltaTime;
            rangeObj.localScale += Vector3.one * (Time.deltaTime / duration);
            yield return oneFrame;
        }
        rangeParent.gameObject.SetActive(false);
        callback.Invoke();
    }

    public void DotAction(int duration, UnityAction onDot, UnityAction onFinish = null)
    {
        StartCoroutine(CoDotAttack(duration, onDot, onFinish));
    }

    private IEnumerator CoDotAttack(int duration, UnityAction onDot, UnityAction onFinish = null)
    {
        WaitForSeconds wait = new WaitForSeconds(1);

        for (int i = 0; i < duration; i++)
        {
            onDot.Invoke();
            yield return wait;
        }

        onFinish?.Invoke();
    }
}