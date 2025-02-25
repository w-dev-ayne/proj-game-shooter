using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Popup : UI_Base
{
    [SerializeField] private bool animation = true;
    public delegate void loadHandler();
    public event loadHandler onImageLoaded;
    
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Managers.UI.SetCanvas(gameObject, true);

        /*onImageLoaded += OnImageLoad;
        SetAllImageResources();
        this.transform.GetChild(0).gameObject.SetActive(false);*/

        //SetButtonSound();
        if(animation)
            SetAnimation();
        return true;
    }

    public virtual void AfterInStackInit()
    {
        
    }

    void SetButtonSound()
    {
        foreach (Button button in FindObjectsOfType<Button>())
        {
            AudioClip buttonSound = Resources.Load("Button.wav", typeof(AudioClip)) as AudioClip;
            
            Debug.Log($"Button : {button} | Sound : {buttonSound}");
            button.onClick.AddListener(() => {Managers.Sound.PlayAudioClip(Define.Sound.Effect, buttonSound);});
        }
    }

    private void SetAnimation()
    {
        if (!this.transform.GetChild(0).GetChild(0).TryGetComponent<UIAppearAnimA>(out UIAppearAnimA animA))
        {
            this.transform.GetChild(0).GetChild(0).AddComponent<UIAppearAnimA>();
        }
    }

    private void OnImageLoad()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
    }

    public virtual void ClosePopupUI()
    {
        if (animation)
        {
            this.transform.GetChild(0).GetChild(0).GetComponent<UIAppearAnimA>().DisappearAnim(() =>
            {
                
                Managers.UI.ClosePopupUI(this);
            });
        }
        else
        {
            Managers.UI.ClosePopupUI(this);    
        }
    }

    private void OnDestroy()
    {
        onImageLoaded -= OnImageLoad;
    }
}
