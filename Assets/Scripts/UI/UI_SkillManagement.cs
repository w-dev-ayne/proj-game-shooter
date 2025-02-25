using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class UI_SkillManagement : UI_Popup
{
    [SerializeField] private GameObject skillUIButtonPrefab;
    public SkillUIButton currentSelectedButton;
    
    private SkillDescriptionArea descriptionArea;
    
    enum Objects
    {
        EquippedSkillsObject,
        SkillsObject,
        DescriptionAreaObject,
    }

    enum Buttons
    {
        SkillDrawButton,
        CloseButton,
        SkillUpgradeButton,
        SkillEquipButton,
        BackButton
    }

    enum Texts
    {
        DescriptionText,
        SkillDrawRemainText
    }
    
    public override bool Init()
    {
        BindObject(typeof(Objects));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        
        GetButton((int)Buttons.SkillDrawButton).gameObject.BindEvent(OnClickSkillDrawButton);
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(ClosePopupUI);
        GetButton((int)Buttons.SkillUpgradeButton).gameObject.BindEvent(OnClickSkillUpgradeButton);
        GetButton((int)Buttons.SkillEquipButton).gameObject.BindEvent(OnClickEquipButton);
        GetButton((int)Buttons.BackButton).gameObject.BindEvent(OnClickBackButton);

        descriptionArea = GetObject((int)Objects.DescriptionAreaObject).GetComponent<SkillDescriptionArea>();

        GetText((int)Texts.SkillDrawRemainText).text = Managers.UserInfo.data.skilldrawPoint.ToString();
        
        LoadSkillData();
        
        if (!base.Init())
            return false;
        return true;
    }

    public override void OnFocus()
    {
        GetText((int)Texts.SkillDrawRemainText).text = Managers.UserInfo.data.skilldrawPoint.ToString();
    }

    public void LoadSkillData()
    {
        SkillData[] equippedDatas = Managers.Skill.equippedSkills;
        Transform equippedSkillButtons = GetObject((int)Objects.EquippedSkillsObject).transform;
        for (int i = 0; i < equippedDatas.Length; i++)
        {
            equippedSkillButtons.GetChild(i).GetComponent<SkillUIButton>().Initialize(equippedDatas[i]);
        }
        
        SkillData[] datas = Managers.Skill.skills;
        Transform skillButtons = GetObject((int)Objects.SkillsObject).transform;
        for (int i = 0; i < datas.Length; i++)
        {
            skillButtons.GetChild(i).GetComponent<SkillUIButton>().Initialize(datas[i]);
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
        descriptionArea.Initialize(button.skillData);
        //SetDescriptionText(currentSelectedButton.skillData);
        Managers.SkillUpgrade.SetSkillData(currentSelectedButton.skillData);
        
        if (currentSelectedButton.skillData.isEquipped && Managers.Skill.equipReadySkill != null)
        {
            if (!Managers.Skill.equipReadySkill.isEquipped)
            {
                SkillEquipNetworkData data = new SkillEquipNetworkData(
                    Managers.Skill.equipReadySkill.id,
                    currentSelectedButton.skillData.id);
                Managers.Skill.EquipSkill(data);
            }
            
            foreach (Transform eButton in GetObject((int)Objects.EquippedSkillsObject).transform)
            {
                eButton.GetComponent<SkillUIButton>().arrow.SetActive(false);
            }

            Managers.Skill.equipReadySkill = null;
        }
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
                             $"COOLTIME : {data.cooltime}\n";
        GetText((int)Texts.DescriptionText).text = description;
    }

    private void OnClickSkillDrawButton()
    {
        if (Managers.UserInfo.data.skilldrawPoint == 0)
        {
            Debug.Log("skilldraw point is 0");
            Managers.UI.ShowPopupUI<UI_Error>().SetErrorText("Skilldraw point is 0");
            return;
        }
        
        Managers.UI.ShowPopupUI<UI_SkillDraw>();
    }

    private void OnClickSkillUpgradeButton()
    {
        Managers.UI.ShowPopupUI<UI_SkillUpgrade>();
        Managers.SkillUpgrade.Init();
    }

    private void OnClickEquipButton()
    {
        if (!currentSelectedButton.skillData.isEquipped)
        {
            Managers.Skill.equipReadySkill = currentSelectedButton.skillData;
            
            foreach (Transform eButton in GetObject((int)Objects.EquippedSkillsObject).transform)
            {
                eButton.GetComponent<SkillUIButton>().arrow.SetActive(true);
            }
        }
    }

    private void OnClickBackButton()
    {
        ClosePopupUI();
    }
}
