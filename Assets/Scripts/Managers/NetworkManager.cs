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
        
        Debug.Log(host);
        token = config.token;
    }

    // 로컬 보유 토큰값 변경
    public void SetToken(string value)
    {
        token = value;
    }

    public void StartLoading()
    {
        Managers.UI.ShowPopupUI<UI_Loading>();
    }

    public void FinishLoading()
    {
        Managers.UI.FindPopup<UI_Loading>()?.ClosePopupUI();
    }

    public void StartSceneLoading()
    {
        Managers.UI.ShowPopupUI<UI_SceneLoading>();
    }

    public void FinishSceneLoading()
    {
        Managers.UI.FindPopup<UI_SceneLoading>()?.ClosePopupUI();
    }
}
