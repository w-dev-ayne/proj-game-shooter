using UnityEngine;

public class GameScene : BaseScene
{
    protected override bool Init()
    {
        if(base.Init() == false)
            return false;

        SceneType = Define.Scene.Game;
        Debug.Log($"{SceneType} Init");

        return true;
    }
}
