using UnityEngine;
using UnityEngine.PlayerLoop;

public class UI_SkillManagement : UI_Popup
{
    [SerializeField] private GameObject skillUIButtonPrefab;
    
    enum Objects
    {
        
    }

    enum Buttons
    {
        
    }

    enum Texts
    {
        DescriptionText
    }
    
    public override bool Init()
    {
        BindObject(typeof(Objects));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        
        
        if (!base.Init())
            return false;
        return true;
    }

    public void SetDescriptionText(SkillData data)
    {
        string description = $"TYPE : {data.type}\n" +
                             $"AMOUNT : {data.amount}\n" +
                             $"COST : {data.cost}\n" +
                             $"RANGE : {data.range}\n" +
                             $"DURATION : {data.duration}\n" +
                             $"DELAY : {data.delay}\n" +
                             $"COOLTIME : {data.coolTime}\n";
        GetText((int)Texts.DescriptionText).text = description;
    }
}
