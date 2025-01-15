using DG.Tweening;
using UnityEngine;

public class UIAnimationManager
{
    public void ButtonClickAnimationA(GameObject buttonObj)
    {
        buttonObj.transform.localScale = Vector3.one * 0.8f;
        buttonObj.transform.DOScale(Vector3.one * 1.0f, 0.5f).SetEase(Ease.OutBounce);
    }
}