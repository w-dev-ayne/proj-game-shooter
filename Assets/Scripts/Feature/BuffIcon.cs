using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BuffIcon : MonoBehaviour
{
    public Image iconImage;
    public Image timerImage;

    public void StartTimer(Skill skill)
    {
        iconImage.sprite = skill.skillIcon;
        StartCoroutine(CoTimer(skill.duration));
    }

    private IEnumerator CoTimer(float duration)
    {
        WaitForEndOfFrame oneFrame = new WaitForEndOfFrame();
        float timer = duration;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            timerImage.fillAmount = timer / duration;
            yield return oneFrame;
        }
        
        FinishTimer();
    }

    private void FinishTimer()
    {
        StopAllCoroutines();
        Destroy(this.gameObject);
    }
}
