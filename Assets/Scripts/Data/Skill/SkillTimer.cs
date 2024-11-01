using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SkillTimer : MonoBehaviour
{
    public void CoolTimer(float coolTime)
    {
        
    }

    private IEnumerator CoolTimerRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);
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