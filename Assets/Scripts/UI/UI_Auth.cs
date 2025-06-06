using UnityEngine;

public class UI_Auth : UI_Popup
{
    enum Objects
    {
        RegisterObject,
        LoginObject
    }

    enum Buttons
    {
        RegisterButton,
        LoginButton,
        LoginRegisterButton,
        SkipButton
    }

    enum Texts
    {
        RegisterUserNameText,
        RegisterIdText,
        RegisterPasswordText,
        RegisterPassword2Text,
        RegisterErrorText,
        LoginIdText,
        LoginPasswordText,
        LoginErrorText
    }
    
    public override bool Init()
    {
        BindObject(typeof(Objects));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        
        GetButton((int)Buttons.RegisterButton).gameObject.BindEvent(OnClickRegisterButton);
        GetButton((int)Buttons.LoginButton).gameObject.BindEvent(OnClickLoginButton);
        GetButton((int)Buttons.LoginRegisterButton).gameObject.BindEvent(OnClickLoginRegisterButton);
        GetButton((int)Buttons.SkipButton).gameObject.BindEvent(OnClickSkipButton);
        
        GetObject((int)Objects.LoginObject).GetComponent<EnterNavigation>().onClickEnter.AddListener(OnClickLoginButton);
        GetObject((int)Objects.RegisterObject).GetComponent<EnterNavigation>().onClickEnter.AddListener(OnClickRegisterButton);
        
        if (!base.Init())
            return false;
        return true;
    }

    private void OnClickRegisterButton()
    {
        string inputUserName = GetText((int)Texts.RegisterUserNameText).text;
        string inputId = GetText((int)Texts.RegisterIdText).text;
        string inputPassword = GetText((int)Texts.RegisterPasswordText).text;
        string inputPassword2 = GetText((int)Texts.RegisterPassword2Text).text;
        
        Managers.Network.authController.Register(inputUserName, inputId, inputPassword, inputPassword2, OnRegisterSuccess, OnRegisterFailed);
    }

    private void OnRegisterSuccess(string message)
    {
        GetObject((int)Objects.LoginObject).gameObject.SetActive(true);
    }

    private void OnRegisterFailed(string message)
    {
        GetText((int)Texts.RegisterErrorText).text = message;
    }

    private void OnClickLoginButton()
    {
        string inputId = GetText((int)Texts.LoginIdText).text;
        string inputPassword = GetText((int)Texts.LoginPasswordText).text;
        Managers.Network.authController.Login(inputId, inputPassword, OnLoginSuccess, OnLoginFailed);
    }

    private void OnLoginSuccess(string message)
    {
        Debug.Log("Login Success");
        Managers.Network.CApiService.GetCharacterData();
        Managers.UserInfo.GetUserInfo();
        Managers.Scene.ChangeScene(Define.Scene.Lobby);
    }

    private void OnLoginFailed(string message)
    {
        GetText((int)Texts.LoginErrorText).text = message;
    }

    private void OnClickLoginRegisterButton()
    {
        GetObject((int)Objects.LoginObject).gameObject.SetActive(false);
    }

    private void OnClickSkipButton()
    {
        Managers.Scene.ChangeScene(Define.Scene.Lobby);
    }
}
