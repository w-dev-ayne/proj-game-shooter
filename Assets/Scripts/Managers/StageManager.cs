using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.TextCore.Text;

public class StageManager : MonoBehaviour
{
    public int currentLevel { get; private set; } = 0;

    public StageData data;
    public Level[] levels { get; private set; }
    public Level currentLevelData { get; private set; }

    public UnityAction<Level> onCurrentLevelStarted;
    public UnityAction<Level> onCurrentLevelFinished;
    public UnityAction<Level> onCurrentLevelKilledUpdated;

    public CharacterController cc;
    public SkillTimer skillTimer;

    public int statCount = 0; 

    void Awake()
    {
        
        Init();
        Debug.Log("Hello I'm Stage Manager");
    }

    private void Init()
    {
        Application.targetFrameRate = 120;
        //base.Awake();
        cc = FindAnyObjectByType<CharacterController>();

        this.levels = new Level[data.levels.Length];

        for (int i = 0; i < data.levels.Length; i++)
        {
            this.levels[i] = new Level(data.levels[i]);
        }

        statCount = 0;
        StartNextLevel();
    }

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

    private void FinishStage()
    {
        Debug.Log("FinishStage");
        Managers.Stat.SetCurrentPoint(statCount);
        Managers.Scene.ChangeScene(Define.Scene.Lobby);
    }

    private void StartNextLevel()
    {
        currentLevel++;

        if (currentLevel == levels.Length + 1)
        {
            FinishStage();
            return;
        }
        
        currentLevelData = levels[currentLevel - 1];
        Managers.Enemy.factory.GenerateEnemies(currentLevelData);
        
        Debug.Log($"Starting stage {currentLevel}");
        onCurrentLevelStarted?.Invoke(currentLevelData);
    }

    private void FinishCurrentLevel()
    {
        Managers.UI.ShowPopupUI<UI_LevelClear>();
        onCurrentLevelFinished?.Invoke(currentLevelData);
        StartNextLevel();
    }
}