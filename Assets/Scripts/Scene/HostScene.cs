using UnityEngine;

public class HostScene : BaseScene
{
    void Awake()
    {

    }
    
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        SceneType = Define.Scene.Host;
        Debug.Log($"{SceneType} Init");

        return true;
    }
}
