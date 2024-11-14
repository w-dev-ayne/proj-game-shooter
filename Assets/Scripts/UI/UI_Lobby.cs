using UnityEngine;

public class UI_Lobby : UI_Popup
{
    enum Buttons
    {
        GameStartButton,
        SkillButton,
        StatButton,
    }
    
    public override bool Init()
    {
        BindButton(typeof(Buttons));
        
        GetButton((int)Buttons.GameStartButton).gameObject.BindEvent(OnClickGameStartButton);
        GetButton((int)Buttons.StatButton).gameObject.BindEvent(OnClickStatButton);
        
        return base.Init();
    }

    private void OnClickGameStartButton()
    {
        Managers.Scene.ChangeScene(Define.Scene.Game);
    }

    private void OnClickStatButton()
    {
        Managers.UI.ShowPopupUI<UI_Stat>();
    }
}
