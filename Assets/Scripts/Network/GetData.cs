using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[System.Serializable]
public class GetData
{
    public bool success;
    public Error error;
    public string msg;
    public string data;
    public string token;
    
    [System.Serializable]
    public partial class Error
    {
        public string code;
        public string msg;
    }

    public void Print()
    {
        Debug.Log($"Success : {success}");
        Debug.Log($"Error : {error}");
        Debug.Log($"Message : {msg}");
        Debug.Log($"Data : {data}");
        Debug.Log($"Token : {token}");
    }
}

