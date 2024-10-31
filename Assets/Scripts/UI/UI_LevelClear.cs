using UnityEngine;

public class UI_LevelClear : UI_Popup
{
    public override bool Init()
    {
        Invoke("ClosePopupUI", 1.0f);
        if (base.Init() == false)
            return false;
        return true;
    }
}
