using UnityEngine;

public class UI_SkillUpgrade : UI_Popup
{
    enum Objects
    {
        SkillInfoObject
    }

    enum Buttons
    {
        CompleteButton,
        UndoButton,
        CancelButton,
        CloseButton
    }

    enum Texts
    {
        CurrentPointText
    }

    public override bool Init()
    {
        BindObject(typeof(Objects));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        
        
        GetButton((int)Buttons.CompleteButton).gameObject.BindEvent(OnClickCompleteButton);
        GetButton((int)Buttons.UndoButton).gameObject.BindEvent(OnClickUndoButton);
        GetButton((int)Buttons.CancelButton).gameObject.BindEvent(OnClickCancelButton);
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(ClosePopupUI);

        GetText((int)Texts.CurrentPointText).text = Managers.SkillUpgrade.currentPoint.ToString();
        Managers.SkillUpgrade.onPointChange += UpdateCurrentPointText;
        //GetButton((int)Buttons.CompleteButton)

        return base.Init();
    }

    public Transform GetSkillInfoObject()
    {
        return GetObject((int)Objects.SkillInfoObject).transform;
    }

    private void OnClickCompleteButton()
    {
        Managers.SkillUpgrade.ApplyAllCommands();
        ClosePopupUI();
    }

    private void OnClickUndoButton()
    {
        Managers.SkillUpgrade.PopCommand();
    }

    private void OnClickCancelButton()
    {
        Managers.SkillUpgrade.PopAllCommands();
    }

    private void UpdateCurrentPointText(int currentPoint)
    {
        GetText((int)Texts.CurrentPointText).text = currentPoint.ToString();
    }
}
