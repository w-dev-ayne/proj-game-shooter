using UnityEngine;

public class NetworkManager : Singleton<NetworkManager>
{
    [SerializeField] private CharacterData cData;
    
    public AuthController authController;
    public CharacterDataController cDataController;

    void Awake()
    {
        Init();
    }

    void Init()
    {
        authController = new AuthController();
        cDataController = new CharacterDataController(this.cData);
    }
}
