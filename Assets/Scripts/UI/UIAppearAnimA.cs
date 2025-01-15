using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class UIAppearAnimA : MonoBehaviour
{
    [SerializeField] private bool playOnAwake = true;

    public void Play()
    {
        this.transform.localScale = Vector3.one * 0.8f;
        this.transform.DOScale(Vector3.one * 1.0f, 0.5f).SetEase(Ease.OutBack);
    }
    
    private void OnEnable()
    {
        if (playOnAwake)
            Play();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
