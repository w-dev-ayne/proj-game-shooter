using UnityEngine;

public class LobbyScene : BaseScene
{
    void Awake()
    {
        Managers.UI.ShowPopupUI<UI_Lobby>();
        Application.targetFrameRate = 120;
    }
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        SceneType = Define.Scene.Lobby;
        Debug.Log($"{SceneType} Init");
        
        Managers.UserInfo.GetUserInfo();
        Managers.Character.Initialize();
        Managers.Skill.Initialize();

        return true;
    }
}
