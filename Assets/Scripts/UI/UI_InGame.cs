using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UI_InGame : UI_Popup
{
    enum Objects
    {
        KilledEnemyObject,
        CurrentLevelObject,
    }

    enum Buttons
    {
        ReloadButton,
        GetTextureButton,
        CaptureButton
    }

    public override bool Init()
    {
        BindObject(typeof(Objects));
        BindButton(typeof(Buttons));
        
        StageManager stageManager = FindAnyObjectByType<StageManager>();
        stageManager.onCurrentLevelStarted += UpdateCurrentLevel;
        stageManager.onCurrentLevelKilledUpdated += UpdateCurrentEnemies;
        
        if (base.Init() == false)
            return false;
        return true;
    }
    

    public void UpdateCurrentEnemies(int currentLevelKilled, Level currentLevelData)
    {
        GetObject((int)Objects.KilledEnemyObject).GetComponent<TextMeshProUGUI>().text =
            $"{currentLevelData.totalEnemiesNum - currentLevelKilled} / {currentLevelData.totalEnemiesNum}";
    }

    public void UpdateCurrentLevel(int currentLevel, Level currentLevelData)
    {
        Debug.Log("Do");
        GetObject((int)Objects.CurrentLevelObject).GetComponent<TextMeshProUGUI>().text = currentLevel.ToString();
        GetObject((int)Objects.KilledEnemyObject).GetComponent<TextMeshProUGUI>().text =
            $"{currentLevelData.totalEnemiesNum} / {currentLevelData.totalEnemiesNum}";
    }

    void OnDestroy()
    {
        StageManager stageManager = FindAnyObjectByType<StageManager>();
        stageManager.onCurrentLevelStarted -= UpdateCurrentLevel;
        stageManager.onCurrentLevelKilledUpdated -= UpdateCurrentEnemies;
    }
}