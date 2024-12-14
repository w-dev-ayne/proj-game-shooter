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
        SkillDrawButton
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
        
        GetButton((int)Buttons.SkillDrawButton).gameObject.BindEvent(OnClickSkillDrawButton);
        
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

    private void OnClickSkillDrawButton()
    {
        Managers.UI.ShowPopupUI<UI_SkillDraw>();
    }
}
