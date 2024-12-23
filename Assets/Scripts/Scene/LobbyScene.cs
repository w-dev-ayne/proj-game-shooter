using UnityEngine;

public class LobbyScene : BaseScene
{
    void Awake()
    {
        Managers.UI.ShowPopupUI<UI_Lobby>();
    }
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        SceneType = Define.Scene.Lobby;
        Debug.Log($"{SceneType} Init");
        
        Managers.Network.cDataController.GetCharacterData();
        Managers.Network.skillController.GetUserSkills();

        return true;
    }
}
