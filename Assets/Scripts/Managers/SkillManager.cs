using UnityEngine;
using UnityEngine.Events;

public class SkillManager
{
    public SkillData[] skills;
    public SkillData[] equippedSkills = new SkillData[4];
    public SkillUpgradeConfiguration config { get; private set; } = new SkillUpgradeConfiguration();
    public SkillData equipReadySkill;

    public void Initialize()
    {
        Managers.Network.skillController.GetUserSkills();
        Managers.Network.skillController.GetSkillConfiguration();
    }

    // 서버로부터 유저 스킬 데이터 불러오기
    public async void GetUserSkills(UnityAction onSuccess = null)
    {
        if (await Managers.Network.skillController.GetUserSkills())
        {
            onSuccess?.Invoke();
        }
    }

    // 서버로부터 받아온 스킬 데이터 로컬 데이터로 동기화
    public void FetchSkill(SkillNetworkData[] skills)
    {
        this.skills = new SkillData[skills.Length - 4];
        int count = 0;
        
        for (int i = 0; i < skills.Length; i++)
        {
            SkillNetworkData currentSkill = skills[i];
            // 장착된 스킬은 장착
            if (currentSkill.isEquipped == "Y")
            {
                this.equippedSkills[currentSkill.equipIndex] = new SkillData(currentSkill);
            }
            else
            {
                this.skills[count] = new SkillData(currentSkill);
                count++;
            }
        }
    }

    // 서버로부터 받아온 스킬 업그레이드 설정 데이터 로컬 데이터로 동기화
    public void FetchConfigData(ConfigurationNetworkData[] serverConfig)
    {
        this.config.FetchData(serverConfig);
    }

    // 스킬 뽑기
    public async void DrawSkill()
    {
        GetData<SkillNetworkData> response = await Managers.Network.skillController.DrawSkill();

        if (response.success)
        {
            Managers.UserInfo.GetUserInfo(() =>
            {
                Managers.UI.FindPopup<UI_SkillDraw>().SetSkillDrawRemainText();
            });

            SkillData drewSkill = new SkillData(response.data);
            Managers.UI.FindPopup<UI_SkillDraw>().SetDrewSkillObject(drewSkill);
        }
        else
        {
            Managers.UI.ShowPopupUI<UI_Error>().SetErrorText(response.error.ToString());
        }
    }

    // 서버에 스킬 업그레이드 API 호출
    public async void UpgradeSkill(SkillUpgradeNetworkData data)
    {
        bool success = await Managers.Network.skillController.UpgradeSkill(data);
        
        if (success)
        {
            GetUserSkills(() =>
            {
                Managers.UI.FindPopup<UI_SkillManagement>().LoadSkillData();
                Managers.UI.FindPopup<UI_SkillManagement>().OnClickSkillButton();
            });
        }
    }

    // 스킬 장착 변경 후 서버에 API 호출
    public async void EquipSkill(SkillEquipNetworkData data)
    {
        bool success = await Managers.Network.skillController.EquipSkill(data);

        if (success)
        {
            GetUserSkills(() =>
            {
                Managers.UI.FindPopup<UI_SkillManagement>().LoadSkillData();
                //Managers.UI.FindPopup<UI_SkillManagement>().OnClickSkillButton();
            });
        }
    }

    // 로컬 장착 스킬 개수가 4개인지 판별 (false이면 게임 시작 안됨)
    public bool IsEquippedSkillReady()
    {
        return this.equippedSkills.Length == 4;
    }
}
