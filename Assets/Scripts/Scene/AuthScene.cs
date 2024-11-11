using UnityEngine;

public class AuthScene : BaseScene
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        SceneType = Define.Scene.Auth;
        Debug.Log($"{SceneType} Init");

        return true;
    }
}
