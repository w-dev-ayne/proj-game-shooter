using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UI_InGame : UI_Popup
{
    private float maxBarWidth;
    
    enum Objects
    {
        EnemyNumBarObject,
    }

    enum Buttons
    {
        ReloadButton,
        GetTextureButton,
        CaptureButton
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
        
        StageManager stageManager = FindAnyObjectByType<StageManager>();
        stageManager.onCurrentLevelStarted += UpdateCurrentLevel;
        stageManager.onCurrentLevelKilledUpdated += UpdateCurrentEnemies;

        CharacterController cc = FindAnyObjectByType<CharacterController>();
        cc.onStatusChanged += UpdateCharacterInfo;

        maxBarWidth = GetObject((int)Objects.EnemyNumBarObject).GetComponent<RectTransform>().sizeDelta.x;
        
        if (base.Init() == false)
            return false;
        return true;
    }
    

    public void UpdateCurrentEnemies(int currentLevelKilled, Level currentLevelData)
    {
        GetText((int)Texts.RemainEnemyText).text =
            $"{currentLevelData.totalEnemiesNum} / {currentLevelData.totalEnemiesNum}";
        
        // Enemy Status Bar Width 변경
        float oneEnemyWidth = maxBarWidth / currentLevelData.totalEnemiesNum;
        RectTransform barRect = GetObject((int)Objects.EnemyNumBarObject).GetComponent<RectTransform>();
        barRect.sizeDelta =
            new Vector2(oneEnemyWidth * (currentLevelData.totalEnemiesNum - currentLevelKilled), barRect.sizeDelta.y);
    }

    public void UpdateCurrentLevel(int currentLevel, Level currentLevelData)
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

    public void UpdateCharacterInfo(CharacterController cc)
    {
        GetText((int)Texts.HPText).text = cc.hp.ToString();
        GetText((int)Texts.AttackText).text = cc.attack.ToString();
        GetText((int)Texts.AttackSpeedText).text = cc.attackSpeed.ToString();
        GetText((int)Texts.MoveSpeedText).text = cc.moveSpeed.ToString();
        GetText((int)Texts.RotateSpeedText).text = cc.rotateSpeed.ToString();
        GetText((int)Texts.BulletSpeedText).text = cc.bulletSpeed.ToString();
    }

    public void UpdateCharacterInfo(Level level)
    {
        
    }

    public void SetSkillInfo(Skill skill)
    {
        
    }

    void OnDestroy()
    {
    }
}