using UnityEngine;

public class UI_Lobby : UI_Popup
{
    enum Buttons
    {
        GameStartButton
    }
    
    public override bool Init()
    {
        BindButton(typeof(Buttons));
        
        GetButton((int)Buttons.GameStartButton).gameObject.BindEvent(OnClickGameStartButton);
        
        return base.Init();
    }

    private void OnClickGameStartButton()
    {
        Managers.Scene.ChangeScene(Define.Scene.Game);
    }
}
