using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillTimer : MonoBehaviour
{
    public void CoolTimer(Skill skill, float coolTime, Image coolTimeImage)
    {
        skill.isCoolTime = true;
        StartCoroutine(CoolTimerRoutine(skill, coolTime, coolTimeImage));
    }

    private IEnumerator CoolTimerRoutine(Skill skill, float coolTime, Image coolTimeImage)
    {
        WaitForEndOfFrame oneFrame = new WaitForEndOfFrame();
        coolTimeImage.gameObject.SetActive(true);
        float timer = coolTime;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            coolTimeImage.fillAmount = timer / coolTime;
            yield return oneFrame;
        }
        
        coolTimeImage.gameObject.SetActive(false);
        skill.isCoolTime = false;
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