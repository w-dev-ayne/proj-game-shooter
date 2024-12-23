using UnityEngine;

[CreateAssetMenu(fileName = "NetworkConfig", menuName = "ScriptableObjects/NetworkConfig")]
public class NetworkConfig : ScriptableObject
{
    public string host;
    public string port;
    [Multiline (4)] public string token;
}
