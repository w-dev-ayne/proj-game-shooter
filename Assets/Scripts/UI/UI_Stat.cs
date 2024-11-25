using UnityEngine;

public class UI_Stat : UI_Popup
{
    enum Objects
    {
        CharacterInfoObject
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

        GetText((int)Texts.CurrentPointText).text = Managers.Stat.currentPoint.ToString();
        Managers.Stat.onPointChange += UpdateCurrentPointText;
        
        return base.Init();
    }

    public override void AfterInStackInit()
    {
        Managers.Stat.Init();
    }

    public Transform GetCharacterInfoObject()
    {
        return GetObject((int)Objects.CharacterInfoObject).transform;
    }

    private void OnClickCompleteButton()
    {
        Managers.Stat.ApplyAllCommands();
        ClosePopupUI();
    }

    private void OnClickUndoButton()
    {
        Managers.Stat.PopCommand();
    }

    private void OnClickCancelButton()
    {
        Managers.Stat.PopAllCommands();
    }

    private void UpdateCurrentPointText(int currentPoint)
    {
        GetText((int)Texts.CurrentPointText).text = currentPoint.ToString();
    }
}
