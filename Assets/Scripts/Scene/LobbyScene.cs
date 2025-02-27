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

        Managers.Sound.Play(Define.Sound.Bgm, AudioDefine.LOBBY_SCENE_BGM_DEFAULT, 0.6f);
        Managers.UserInfo.GetUserInfo(Managers.UI.FindPopup<UI_Lobby>().FetchUserInfoData);
        Managers.Character.Initialize();
        Managers.Skill.Initialize();

        return true;
    }
}
