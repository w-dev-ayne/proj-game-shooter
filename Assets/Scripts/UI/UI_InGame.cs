using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_InGame : UI_Popup
{
    [SerializeField] private GameObject buffIconPrefab;
    
    private float maxBarWidth;
    
    public Dictionary<Skill, Button> skillButtons = new Dictionary<Skill, Button>();
    
    enum Objects
    {
        EnemyNumBarObject,
        InstructionObject,
        PointObject,
        GetPointObject,
        BuffStatusObject
    }

    enum Buttons
    {
        SkillAButton = 0,
        SkillBButton = 1,
        SkillCButton = 2,
        SkillDButton = 3,
        StopButton
    }

    enum Texts
    {
        HPText,
        LevelText,
        MPText,
        StatText,
        AttackText,
        AttackSpeedText,
        MoveSpeedText,
        RotateSpeedText,
        BulletSpeedText,
        RemainEnemyText,
        InstructionText,
        PointText
    }

    public override bool Init()
    {
        Debug.Log("INGAME INIT");
        
        BindObject(typeof(Objects));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        maxBarWidth = GetObject((int)Objects.EnemyNumBarObject).GetComponent<RectTransform>().sizeDelta.x;
        
        Debug.Log(Managers.Stage);
        Managers.Stage.onCurrentLevelStarted += UpdateCurrentLevel;
        Managers.Stage.onCurrentLevelKilledUpdated += UpdateCurrentEnemies;
        Managers.Stage.cc.onStatusChanged += UpdateCharacterInfo;

        //UpdateCurrentLevel(Managers.Stage.currentLevelData);
        UpdateCharacterInfo(Managers.Stage.cc);
        SetSkillButtons(Managers.Stage.cc.skills);
        
        GetObject((int)Objects.InstructionObject).SetActive(false);
        Managers.Instruction.Init(GetObject((int)Objects.InstructionObject), GetText((int)Texts.InstructionText));
        
        GetButton((int)Buttons.StopButton).gameObject.BindEvent(OnClickStopButton);
        
        if (base.Init() == false)
            return false;
        return true;
    }

    // Skill 버튼에 각 Skill 할당 및 Skill 쿨타임 이미지 오브젝트 할당
    public void SetSkillButtons(Skill[] skills)
    {
        Debug.Log(skills.Length);
        for (int i = 0; i < skills.Length; i++)
        {
            skillButtons[skills[i]] = GetButton(i);
            Skill skill = skills[i]; // 값 복사해서 스킬버튼에 할당하기
            GetButton(i).gameObject.BindEvent(() => OnClickSkillButton(skill));
            if(skill.skillIcon != null)
                GetButton(i).transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = skill.skillIcon;
            skills[i].coolTimeImage = GetButton(i).transform.GetChild(1).GetComponent<Image>();
        }
    }
    
    // 적 처치 시 UI 업데이트
    private void UpdateCurrentEnemies(Level currentLevelData)
    {
        GetText((int)Texts.RemainEnemyText).text =
            $"{currentLevelData.currentEnemiesNum} / {currentLevelData.totalEnemiesNum}";
        GetText((int)Texts.StatText).text = Managers.Stage.characterPoint.ToString();
        // Enemy Status Bar Width 변경
        float oneEnemyWidth = maxBarWidth / currentLevelData.totalEnemiesNum;
        RectTransform barRect = GetObject((int)Objects.EnemyNumBarObject).GetComponent<RectTransform>();
        barRect.sizeDelta =
            new Vector2(oneEnemyWidth * (currentLevelData.currentEnemiesNum), barRect.sizeDelta.y);
    }

    // Level 변경 시 UI 업데이트
    public void UpdateCurrentLevel(Level currentLevelData)
    {
        GetText((int)Texts.LevelText).text = Managers.Stage.currentLevel.ToString();
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

    public void AddPointAnimation(Vector3 point)
    {
        GetObject((int)Objects.GetPointObject).transform.position = point;
        
        GetObject((int)Objects.GetPointObject).transform
            .DOMove(GetObject((int)Objects.PointObject).transform.position, 0.5f).OnComplete(
                () =>
                {
                    SetPointText();
                });
    }

    private void SetPointText()
    {
        GetText((int)Texts.PointText).text = Managers.Stage.characterPoint.ToString();
    }

    public void StartBuffIcon(Skill skill)
    {
        GameObject buffIcon = Instantiate(buffIconPrefab);
        buffIcon.transform.SetParent(GetObject((int)Objects.BuffStatusObject).transform);
        buffIcon.GetComponent<BuffIcon>().StartTimer(skill);
    }

    private void OnClickStopButton()
    {
        Managers.Scene.ChangeScene(Define.Scene.Lobby);
    }
}