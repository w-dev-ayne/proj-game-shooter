using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class AuthController : APILoader
{
    public async void Register(string username, string id, string password, string passwowrd2, UnityAction<string> OnRegisterSuccess = null, UnityAction<string> OnRegisterFail = null)
    {
        RegisterAuthData data = new RegisterAuthData(username, id, password, passwowrd2);
        
        GetData message = await base.PostAPI($"{NetworkDefine.Host}/auth/register", data);
        
        if (message.success)
        {
            OnRegisterSuccess?.Invoke(message.data);
        }
        else
        {
            OnRegisterFail?.Invoke(message.error.msg);
        }
    }
        
    public async void Login(string inputId, string inputPassword, UnityAction<string> OnLoginSuccess = null, UnityAction<string> OnLoginFail = null)
    {
        LoginAuthData data = new LoginAuthData(inputId, inputPassword);
        
        GetData message = await base.PostAPI($"{NetworkDefine.Host}/auth/login", data);
        
        if (message.success)
        {
            base.SetToken(message.token);
            OnLoginSuccess?.Invoke("Success");    
        }
        else
        {
            OnLoginFail?.Invoke(message.error.msg);
        }
    }

    public void Logout()
    {
        
    }
}