using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public class Managers : MonoBehaviour
{
    private static Managers s_instance = null;
    public static Managers Instance { get { return s_instance; } }

    private static UIManager s_uiManager = new UIManager();
    private static ResourceManager s_resourceManager = new ResourceManager();
    private static SceneManagerEx s_sceneManager = new SceneManagerEx();
    private static SoundManager s_soundManager = new SoundManager();

    private static EnemyManager enemyManager = new EnemyManager();
    private static StageManager stageManager = new StageManager();
    private static InstructionManager instructionManager = new InstructionManager();
    private static StatManager statManager = new StatManager();
    
    
    public static UIManager UI { get { Init(); return s_uiManager; } }
    public static ResourceManager Resource { get { Init(); return s_resourceManager; } }
    public static SceneManagerEx Scene { get { Init(); return s_sceneManager; } }
    public static SoundManager Sound {  get { Init(); return s_soundManager; } }
    
    
    public static EnemyManager Enemy { get { Init(); return enemyManager; } }
    public static StageManager Stage { get { Init(); return stageManager; } }
    public static InstructionManager Instruction {get {Init(); return instructionManager;}}
    public static StatManager Stat { get { Init(); return statManager; } }
    
    
    
    private void Awake() 
    {
        //Init();
    }

    private static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
                go = new GameObject { name = "@Managers" };

            s_instance = Utils.GetOrAddComponent<Managers>(go);
            DontDestroyOnLoad(go);

            s_resourceManager.Init();
            s_sceneManager.Init();
            s_soundManager.Init();

            Application.targetFrameRate = 60;
        }
    }

    public static void SceneInit()
    {
        switch (s_sceneManager.CurrentSceneType)
        {
            case Define.Scene.Auth:
                break;
            case Define.Scene.Lobby:
                
                if(StatManager.Instance == null)
                    statManager = InstantiateMonoBehaviourManager<StatManager>();
                break;
            case Define.Scene.Game:
                enemyManager = InstantiateMonoBehaviourManager<EnemyManager>();
                stageManager = InstantiateMonoBehaviourManager<StageManager>();
                instructionManager = InstantiateMonoBehaviourManager<InstructionManager>();
                break;
        }
    }

    private static T InstantiateMonoBehaviourManager<T>() where T : class
    {
        GameObject managerObj = s_resourceManager.Instantiate($"Managers/@{typeof(T).ToString()}");
        return managerObj.GetComponent<T>();
    }
}
