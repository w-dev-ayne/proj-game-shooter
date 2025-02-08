using UnityEngine;

public class UI_Error : UI_Popup
{
    enum Buttons
    {
        CloseButton
    }
    
    enum Texts
    {
        ErrorText
    }

    public override bool Init()
    {
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnClickCloseButton);
        
        if (base.Init())
            return false;
        return true;
    }

    public void SetErrorText(string message)
    {
        GetText((int)Texts.ErrorText).text = message;
    }

    private void OnClickCloseButton()
    {
        ClosePopupUI();
    }
}
