using UnityEngine;

public class UI_Host : UI_Popup
{
    [SerializeField] private NetworkConfig config;
    
    enum Objects
    {
        
    }

    enum Buttons
    {
        SubmitButton,
        SkipButton
    }

    enum Texts
    {
        HostText
    }

    public override bool Init()
    {
        BindObject(typeof(Objects));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        
        GetButton((int)Buttons.SubmitButton).gameObject.BindEvent(OnClickSubmitButton);
        GetButton((int)Buttons.SkipButton).gameObject.BindEvent(OnClickSkipButton);

        if (!base.Init())
            return false;
        return true;
    }

    private void OnClickSubmitButton()
    {
        config.host = GetText((int)Texts.HostText).text;
        Managers.Scene.ChangeScene(Define.Scene.Auth);
    }

    private void OnClickSkipButton()
    {
        Managers.Scene.ChangeScene(Define.Scene.Auth);
    }
}
