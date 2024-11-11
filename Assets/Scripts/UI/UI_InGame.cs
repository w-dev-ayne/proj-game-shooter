using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_InGame : UI_Popup
{
    [SerializeField] private GameObject skillButtonPrefab;
    private float maxBarWidth;
    
    public Dictionary<Skill, Button> skillButtons = new Dictionary<Skill, Button>();
    
    enum Objects
    {
        EnemyNumBarObject,
    }

    enum Buttons
    {
        SkillAButton = 0,
        SkillBButton = 1,
        SkillCButton = 2,
        SkillDButton = 3
    }

    enum Texts
    {
        HPText,
        LevelText,
        MPText,
        AttackText,
        AttackSpeedText,
        MoveSpeedText,
        RotateSpeedText,
        BulletSpeedText,
        
        RemainEnemyText,
    }

    public override bool Init()
    {
        BindObject(typeof(Objects));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        
        Managers.Stage.onCurrentLevelStarted += UpdateCurrentLevel;
        Managers.Stage.onCurrentLevelKilledUpdated += UpdateCurrentEnemies;
        Managers.Stage.cc.onStatusChanged += UpdateCharacterInfo;

        maxBarWidth = GetObject((int)Objects.EnemyNumBarObject).GetComponent<RectTransform>().sizeDelta.x;
        
        if (base.Init() == false)
            return false;
        return true;
    }

    // Skill 버튼에 각 Skill 할당 및 Skill 쿨타임 이미지 오브젝트 할당
    public void SetSkillButtons(Skill[] skills)
    {
        for (int i = 0; i < skills.Length; i++)
        {
            skillButtons[skills[i]] = GetButton(i);
            Skill skill = skills[i]; // 값 복사해서 스킬버튼에 할당하기
            GetButton(i).gameObject.BindEvent(() => OnClickSkillButton(skill));
            skills[i].coolTimeImage = GetButton(i).transform.GetChild(2).GetComponent<Image>();
        }
    }
    
    // 적 처치 시 UI 업데이트
    private void UpdateCurrentEnemies(int currentLevelKilled, Level currentLevelData)
    {
        GetText((int)Texts.RemainEnemyText).text =
            $"{currentLevelData.totalEnemiesNum} / {currentLevelData.totalEnemiesNum}";
        
        // Enemy Status Bar Width 변경
        float oneEnemyWidth = maxBarWidth / currentLevelData.totalEnemiesNum;
        RectTransform barRect = GetObject((int)Objects.EnemyNumBarObject).GetComponent<RectTransform>();
        barRect.sizeDelta =
            new Vector2(oneEnemyWidth * (currentLevelData.totalEnemiesNum - currentLevelKilled), barRect.sizeDelta.y);
    }

    // Level 변경 시 UI 업데이트
    private void UpdateCurrentLevel(int currentLevel, Level currentLevelData)
    {
        Debug.Log("Do");
        GetText((int)Texts.LevelText).text = currentLevel.ToString();
        GetText((int)Texts.RemainEnemyText).text =
            $"{currentLevelData.totalEnemiesNum} / {currentLevelData.totalEnemiesNum}";
        
        // Enemy Status Bar Width 초기화
        RectTransform barRect = GetObject((int)Objects.EnemyNumBarObject).GetComponent<RectTransform>();
        barRect.sizeDelta =
            new Vector2(maxBarWidth, barRect.sizeDelta.y);
    }

    // 캐릭터 정보 업데이트 시 UI 변경
     private void UpdateCharacterInfo(CharacterController cc)
    {
        GetText((int)Texts.HPText).text = cc.hp.ToString();
        GetText((int)Texts.MPText).text = cc.mp.ToString();
        GetText((int)Texts.AttackText).text = cc.attack.ToString();
        GetText((int)Texts.AttackSpeedText).text = cc.attackSpeed.ToString();
        GetText((int)Texts.MoveSpeedText).text = cc.moveSpeed.ToString();
        GetText((int)Texts.RotateSpeedText).text = cc.rotateSpeed.ToString();
        GetText((int)Texts.BulletSpeedText).text = cc.bulletSpeed.ToString();
    }

    private void OnClickSkillButton(Skill skill)
    {
        Managers.Stage.cc.Skill(skill);
    }
}