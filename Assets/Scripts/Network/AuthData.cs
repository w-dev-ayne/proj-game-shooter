using UnityEngine;

public class RegisterAuthData : PostData
{
    public string username;
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