using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.TextCore.Text;

public class StageManager : MonoBehaviour
{
    public int currentLevel { get; private set; } = 0;
    public Level[] levels { get; private set; }
    public Level currentLevelData { get; private set; }

    public UnityAction<Level> onCurrentLevelStarted;
    public UnityAction<Level> onCurrentLevelFinished;
    public UnityAction<Level> onCurrentLevelKilledUpdated;

    public CharacterController cc;
    public SkillTimer skillTimer;

    public int characterPoint { get; private set; }

    void Awake()
    {
        Init();
    }

    void Start()
    {
        Managers.Enemy.GetEnemiesData();
    }

    private void Init()
    {
        Application.targetFrameRate = 120;
        cc = FindAnyObjectByType<CharacterController>();

        this.levels = new Level[5];

        for (int i = 0; i < 5; i++)
        {
            this.levels[i] = new Level();
        }

        characterPoint = 0;
        // StartNextLevel();
    }

    public void InsertEnemies(EnemyNetworkData[] enemies)
    {
        foreach (EnemyNetworkData enemy in enemies)
        {
            levels[enemy.levelId - 1].AddEnemyData(enemy);
        }
        
        StartNextLevel();
        
        //Managers.UI.FindPopup<UI_InGame>().UpdateCurrentLevel(currentLevelData);
    }

    
    // 현재 레벨에 존재하는 Enemy 개수 Update
    public void UpdateEnemyNum()
    {
        currentLevelData.currentEnemiesNum--;
        onCurrentLevelKilledUpdated?.Invoke(currentLevelData);

        if (currentLevelData.currentEnemiesNum == 0)
        {
            FinishCurrentLevel();
        }
    }

    public void StartStage()
    {
        
    }

    // 현재 스테이지 종료
    private void FinishStage()
    {
        Debug.Log("FinishStage");
        Managers.UserInfo.AddCharacterPoint(this.characterPoint);
        Managers.UserInfo.AddSkillDrawPoint();
        Managers.Scene.ChangeScene(Define.Scene.Lobby);
    }

    // 다음 레벨 시작
    private void StartNextLevel()
    {
        currentLevel++;

        if (currentLevel == levels.Length + 1)
        {
            FinishStage();
            return;
        }
        
        currentLevelData = levels[currentLevel - 1];
        if (currentLevel == 1)
        {
            Managers.Enemy.factory.GenerateEnemies(currentLevelData, true);    
        }
        else
        {
            Managers.Enemy.factory.GenerateEnemies(currentLevelData, false);
        }
        
        Debug.Log($"Starting stage {currentLevel}");
        onCurrentLevelStarted?.Invoke(currentLevelData);
    }

    // 현재 레벨 종료
    private void FinishCurrentLevel()
    {
        Managers.UI.ShowPopupUI<UI_LevelClear>();
        onCurrentLevelFinished?.Invoke(currentLevelData);
        StartNextLevel();
    }

    public void GetPoint()
    {
        characterPoint++;
    }
}