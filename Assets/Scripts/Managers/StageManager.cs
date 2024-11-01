using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.TextCore.Text;

public class StageManager : Singleton<StageManager>
{
    private int currentLevel = 0;

    public StageData data;
    public Level[] levels;
    public Level currentLevelData;
    public int currentLevelKilled = 0;

    public EnemyManager enemyManager;

    public UnityAction<int, Level> onCurrentLevelStarted;
    public UnityAction<int, Level> onCurrentLevelFinished;
    public UnityAction<int, Level> onCurrentLevelKilledUpdated;

    public CharacterController cc;
    public SkillTimer skillManager;

    void Awake()
    {
        base.Awake();
        cc = FindAnyObjectByType<CharacterController>();
        Managers.UI.ShowPopupUI<UI_InGame>();

        this.levels = new Level[data.levels.Length];

        for (int i = 0; i < data.levels.Length; i++)
        {
            this.levels[i] = new Level(data.levels[i]);
        }
    }

    void Start()
    {
        StartNextLevel();
    }

    public void UpdateCurrentLevelKill()
    {
        currentLevelKilled++;
        onCurrentLevelKilledUpdated?.Invoke(currentLevelKilled, currentLevelData);

        if (currentLevelKilled == currentLevelData.totalEnemiesNum)
        {
            FinishCurrentLevel();
        }
    }

    public void StartStage()
    {
        
    }

    public void FinishStage()
    {
        
    }

    private void StartNextLevel()
    {
        currentLevel++;
        currentLevelData = levels[currentLevel - 1];
        currentLevelKilled = 0;
        enemyManager.factory.GenerateEnemies(currentLevelData);
        
        Debug.Log($"Starting stage {currentLevel}");
        onCurrentLevelStarted?.Invoke(currentLevel, currentLevelData);
    }

    private void FinishCurrentLevel()
    {
        Managers.UI.ShowPopupUI<UI_LevelClear>();
        onCurrentLevelFinished?.Invoke(currentLevel, currentLevelData);
        StartNextLevel();
    }
}