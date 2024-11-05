using UnityEngine;

public class RegisterAuthData : PostData
{
    public string username;
    public string loginId;
    public string loginPassword;
    public string loginPassword2;

    public RegisterAuthData(string username, string loginId, string loginPassword, string loginPassword2)
    {
        this.username = username;
        this.loginId = loginId;
        this.loginPassword = loginPassword;
        this.loginPassword2 = loginPassword2;
    }
}

public class LoginAuthData : PostData
{
    public string loginId;
    public string loginPassword;

    public LoginAuthData(string loginId, string loginPassword)
    {
        this.loginId = loginId;
        this.loginPassword = loginPassword;
    }

    public void Print()
    {
        Debug.Log(loginId);
        Debug.Log(loginPassword);
    }
}

public class LoginTokenAuthData : GetData
{
    public string token;
}