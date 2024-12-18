using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class AuthController : APILoader
{
    public async void Register(string username, string id, string password, string passwowrd2, UnityAction<string> OnRegisterSuccess = null, UnityAction<string> OnRegisterFail = null)
    {
        RegisterAuthData data = new RegisterAuthData(username, id, password, passwowrd2);
        
        GetData<string> message = await base.PostAPI<string>("/auth/register", data);
        
        if (message.success)
        {
            OnRegisterSuccess?.Invoke(message.data.ToString());
        }
        else
        {
            OnRegisterFail?.Invoke(message.error.msg);
        }
    }
        
    public async void Login(string inputId, string inputPassword, UnityAction<string> OnLoginSuccess = null, UnityAction<string> OnLoginFail = null)
    {
        LoginAuthData data = new LoginAuthData(inputId, inputPassword);
        
        GetData<string> message = await base.PostAPI<string>($"/auth/login", data);
        
        if (message.success)
        {
            Managers.Network.SetToken(message.token);
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