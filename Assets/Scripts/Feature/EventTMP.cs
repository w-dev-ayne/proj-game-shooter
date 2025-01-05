using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EventTMP : MonoBehaviour
{
    private TextMeshProUGUI tmp;

    void Awake()
    {
        this.tmp = this.GetComponent<TextMeshProUGUI>();
        Initialize();
    }

    private void Initialize()
    {
        tmp.DOFade(0, 0);
    }

    public void Animate(string text)
    {
        transform.DOPause();
        transform.localPosition = Vector3.zero;
        tmp.DOFade(1, 0);
        tmp.text = text;
        transform.DOLocalMoveY(0.2f, 0.5f);
        tmp.DOFade(0, 1);
    }
}
