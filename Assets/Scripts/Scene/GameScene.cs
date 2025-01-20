using UnityEngine;

public class GameScene : BaseScene
{
    protected override bool Init()
    {
        if(base.Init() == false)
            return false;

        SceneType = Define.Scene.Game;
        Debug.Log($"{SceneType} Init");
        Managers.UI.ShowPopupUI<UI_InGame>();
        
        Managers.Sound.Play(Define.Sound.Bgm, AudioDefine.GAME_SCENE_BGM_DEFAULT, 0.6f);
        
        return true;
    }
}