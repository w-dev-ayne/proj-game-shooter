using UnityEngine;


public class NetworkManager : Singleton<NetworkManager>
{
    [SerializeField] private CharacterData cData;

    public string host { get; private set; } = "http://localhost:3000";
    public string token { get; private set; } =
        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzZXJ2aWNlIjoiVVNFUiIsInRva2VuVHlwZSI6ImFjY2Vzc1Rva2VuIiwiaWQiOjExLCJpYXQiOjE3MzQ1ODUxNzF9.XSyYL0Zc1yE9ii_www-W4xYHI9FDYiiUe8yN2NYllZ4";

    public AuthController authController;
    public CharacterDataController cDataController;
    public SkillDataController skillController;

    void Awake()
    {
        Debug.Log(host);
        Init();
    }

    void Init()
    {
        authController = new AuthController();
        cDataController = new CharacterDataController(this.cData);
        skillController = new SkillDataController();
    }

    public void SetToken(string value)
    {
        token = value;
    }
}
