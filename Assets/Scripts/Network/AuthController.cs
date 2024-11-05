using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class AuthController : APILoader
{
    public async void Register(string username, string password, string passwowrd2)
    {
            
    }
        
    public async void Login(string inputId, string inputPassword, UnityAction OnLoginSuccess = null, UnityAction OnLoginFail = null)
    {
        LoginAuthData data = new LoginAuthData(inputId, inputPassword);
        
        GetData message = await base.PostAPI($"{NetworkDefine.Host}/auth/login", data);
        
        if (message.success)
        {
            base.SetToken(message.token);
            OnLoginSuccess?.Invoke();    
        }
        else
        {
            OnLoginFail?.Invoke();
        }
    }

    public void Logout()
    {
        
    }
}