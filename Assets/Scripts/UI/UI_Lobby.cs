using UnityEngine;

public class UI_Lobby : UI_Popup
{
    enum Buttons
    {
        GameStartButton,
        SkillButton,
        StatButton,
    }

    enum Texts
    {
        StageText
    }
    
    public override bool Init()
    {
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        
        GetButton((int)Buttons.GameStartButton).gameObject.BindEvent(OnClickGameStartButton);
        GetButton((int)Buttons.StatButton).gameObject.BindEvent(OnClickStatButton);
        GetButton((int)Buttons.SkillButton).gameObject.BindEvent(OnClickSkillButton);
        
        return base.Init();
    }

    private void OnClickGameStartButton()
    {
        if (!Managers.Skill.IsEquippedSkillReady())
        {
            Debug.Log("Equipped Skill Not Enough");
            return;
        }
        Managers.Scene.ChangeScene(Define.Scene.Game);
    }

    private void OnClickStatButton()
    {
        Managers.UI.ShowPopupUI<UI_Stat>();
    }

    private void OnClickSkillButton()
    {
        Managers.UI.ShowPopupUI<UI_SkillManagement>();
    }

    public void FetchUserInfoData()
    {
        GetText((int)Texts.StageText).text = $"Stage {Managers.UserInfo.data.currentStage}";
    }
}
