using UnityEngine;

public class UI_SkillManagement : UI_Popup
{
    [SerializeField] private GameObject skillUIButtonPrefab;
    
    enum Objects
    {
        
    }

    enum Buttons
    {
        
    }

    enum Text
    {
        DescriptionText
    }
    
    public override bool Init()
    {

        if (!base.Init())
            return false;
        return true;
    }
}
