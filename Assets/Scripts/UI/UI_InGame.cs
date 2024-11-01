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
        
        StageManager.Instance.onCurrentLevelStarted += UpdateCurrentLevel;
        StageManager.Instance.onCurrentLevelKilledUpdated += UpdateCurrentEnemies;
        
        StageManager.Instance.cc.onStatusChanged += UpdateCharacterInfo;

        maxBarWidth = GetObject((int)Objects.EnemyNumBarObject).GetComponent<RectTransform>().sizeDelta.x;
        
        GetButton((int)Buttons.SkillAButton).gameObject.BindEvent(() => OnClickSkillButton((int)Buttons.SkillAButton));
        GetButton((int)Buttons.SkillBButton).gameObject.BindEvent(() => OnClickSkillButton((int)Buttons.SkillBButton));
        GetButton((int)Buttons.SkillCButton).gameObject.BindEvent(() => OnClickSkillButton((int)Buttons.SkillCButton));
        GetButton((int)Buttons.SkillDButton).gameObject.BindEvent(() => OnClickSkillButton((int)Buttons.SkillDButton));
        
        if (base.Init() == false)
            return false;
        return true;
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

    private void OnClickSkillButton(int i)
    {
        StageManager.Instance.cc.Skill(i);
    }

    public Image GetCoolTimeImage(int idx)
    {
        return GetButton(idx).transform.GetChild(2).GetComponent<Image>();
    }
}