using UnityEngine;

public class UI_Auth : UI_Popup
{
    private AuthController authController;
    
    enum Objects
    {
        RegisterObject,
        LoginObject
    }

    enum Buttons
    {
        RegisterButton,
        LoginButton
    }

    enum Texts
    {
        RegisterIdText,
        RegisterPasswordText,
        RegisterPassword2Text,
        LoginIdText,
        LoginPasswordText
    }
    
    public override bool Init()
    {
        authController = new AuthController();
        
        BindObject(typeof(Objects));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        
        //GetButton((int)Buttons.RegisterButton).gameObject.BindEvent(OnClickRegisterButton);
        GetButton((int)Buttons.LoginButton).gameObject.BindEvent(OnClickLoginButton);
        
        if (!base.Init())
            return false;
        return true;
    }

    private void OnClickRegisterButton()
    {
        
    }

    private void OnClickLoginButton()
    {
        string inputId = GetText((int)Texts.LoginIdText).text;
        string inputPassword = GetText((int)Texts.LoginPasswordText).text;
        authController.Login(inputId, inputPassword, OnLoginSuccess, OnLoginFailed);
    }

    private void OnLoginSuccess()
    {
        Debug.Log("LoginComplete");
    }

    private void OnLoginFailed()
    {
        Debug.Log("LoginFail");
    }
}
