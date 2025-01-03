using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class UI_SkillManagement : UI_Popup
{
    [SerializeField] private GameObject skillUIButtonPrefab;
    public SkillUIButton currentSelectedButton;
    
    enum Objects
    {
        EquippedSkillsObject,
        SkillsObject
    }

    enum Buttons
    {
        SkillDrawButton,
        CloseButton,
        SkillUpgradeButton
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
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(ClosePopupUI);
        GetButton((int)Buttons.SkillUpgradeButton).gameObject.BindEvent(OnClickSkillUpgradeButton);
        
        LoadSkillData();
        
        if (!base.Init())
            return false;
        return true;
    }

    public void LoadSkillData()
    {
        SkillData[] equippedDatas = Managers.Skill.equippedSkills;
        Transform equippedSkillButtons = GetObject((int)Objects.EquippedSkillsObject).transform;
        for (int i = 0; i < equippedDatas.Length; i++)
        {
            equippedSkillButtons.GetChild(i).GetComponent<SkillUIButton>().skillData = equippedDatas[i];
        }
        
        SkillData[] datas = Managers.Skill.skills;
        Transform skillButtons = GetObject((int)Objects.SkillsObject).transform;
        for (int i = 0; i < datas.Length; i++)
        {
            skillButtons.GetChild(i).GetComponent<SkillUIButton>().skillData = datas[i];
        }
    }

    public void OnClickSkillButton(SkillUIButton button = null)
    {
        if (button.skillData == null)
            return;
        if (button != null)
            currentSelectedButton = button;
        if (currentSelectedButton == null)
            return;
        SetDescriptionText(currentSelectedButton.skillData);
        Managers.SkillUpgrade.SetSkillData(currentSelectedButton.skillData);
    }

    private void SetDescriptionText(SkillData data)
    {
        if (data == null)
        {
            GetText((int)Texts.DescriptionText).text = "null";
            return;
        }
        
        string description = $"SKILL NAME : {data.name}\n" +
                             $"TYPE : {data.type}\n" +
                             $"AMOUNT : {data.amount}\n" +
                             $"COST : {data.cost}\n" +
                             $"RANGE : {data.range}\n" +
                             $"DURATION : {data.duration}\n" +
                             $"VFX ON DELAY : {data.vfxOnDelay}\n" +
                             $"DELAY : {data.delay}\n" +
                             $"COOLTIME : {data.coolTime}\n";
        GetText((int)Texts.DescriptionText).text = description;
    }

    private void OnClickSkillDrawButton()
    {
        Managers.UI.ShowPopupUI<UI_SkillDraw>();
    }

    private void OnClickSkillUpgradeButton()
    {
        Managers.UI.ShowPopupUI<UI_SkillUpgrade>();
        Managers.SkillUpgrade.Init();
    }
}
