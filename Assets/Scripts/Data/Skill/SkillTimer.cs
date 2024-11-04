using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillTimer : MonoBehaviour
{
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
    
    public void BuffTimer(float duration, UnityAction<CharacterController> callback)
    {
        Debug.Log("Start Duration");
        StartCoroutine(BuffTimerRoutine(duration, callback));
    }
    
    private IEnumerator BuffTimerRoutine(float duration, UnityAction<CharacterController> callback)
    {
        yield return new WaitForSeconds(duration);
        Debug.Log("Finish Duration");
        callback.Invoke(StageManager.Instance.cc);
    }
}