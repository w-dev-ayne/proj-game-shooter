using System.Collections;
using UnityEngine;

public class SkillRange : MonoBehaviour
{
    public Transform range;

    private bool isTimer = false;
    private float timer = 0;
    private float delay = 0;
    
    private void Awake()
    {
        
    }
    
    public void StartRange(float delay, float rangeAmount)
    {
        this.transform.localScale = Vector3.one * (2 * rangeAmount);
        range.localScale = Vector3.zero;

        this.timer = 0;
        this.delay = delay;
        isTimer = true;
    }

    private IEnumerator RangeRoutine(float delay, float rangeAmount)
    {
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        float timer = 0;
        Debug.Log(timer);
        while (timer < delay)
        {
            timer += Time.deltaTime;
            range.localScale += Vector3.one * (2 * Time.deltaTime);
            yield return waitForEndOfFrame;
        }
        
        Debug.Log(timer);
    }

    void Update()
    {
        if (isTimer)
        {
            if (timer < delay)
            {
                timer += Time.deltaTime;
                range.localScale += Vector3.one * (Time.deltaTime / delay);
            }
            else
            {
                FinishRange();
            }
        }
    }

    private void FinishRange()
    {
        isTimer = false;
        this.gameObject.SetActive(false);
    }
}
