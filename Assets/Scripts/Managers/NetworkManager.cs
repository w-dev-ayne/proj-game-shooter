using UnityEngine;


public class NetworkManager : Singleton<NetworkManager>
{
    [SerializeField] private NetworkConfig config;

    public string host { get; private set; } = "http://localhost:3000";
    public string token { get; private set; } =
        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzZXJ2aWNlIjoiVVNFUiIsInRva2VuVHlwZSI6ImFjY2Vzc1Rva2VuIiwiaWQiOjEzLCJpYXQiOjE3MzQ5NTc2NzR9.rdbcEFDGEy6dmqRjW-BoPE98AIch_rg2Dc9Q9X34JJg";

    public AuthController authController;
    public CharacterDataController cDataController;
    public SkillDataController skillController;
    public UserInfoDataController userInfoDataController;

    void Awake()
    {
        base.Awake();
        Init();
    }

    void Init()
    {
        authController = new AuthController();
        cDataController = new CharacterDataController();
        skillController = new SkillDataController();
        userInfoDataController = new UserInfoDataController();

        host = $"http://{config.host}:{config.port}";
        token = config.token;
    }

    public void SetToken(string value)
    {
        token = value;
    }
}
