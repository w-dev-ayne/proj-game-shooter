using UnityEngine;

public class UI_SceneLoading : UI_Popup
{
    public override bool Init()
    {
        Invoke("ClosePopupUI", 1.0f);
        if (base.Init() == false)
            return false;
        return true;
    }
}
