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
    private static SkillManager s_skillManager = new SkillManager();
    private static CharacterManager s_characterManager = new CharacterManager();
    private static UserInfoManager s_userInfoManager = new UserInfoManager();
    private static UIAnimationManager s_uiAnimationManager = new UIAnimationManager();

    private static EnemyManager enemyManager;
    private static StageManager stageManager;
    private static InstructionManager instructionManager;
    private static StatManager statManager;
    private static NetworkManager networkManager;
    private static SkillUpgradeManager skillUpgradeManager;
    
    
    
    public static UIManager UI { get { Init(); return s_uiManager; } }
    public static ResourceManager Resource { get { Init(); return s_resourceManager; } }
    public static SceneManagerEx Scene { get { Init(); return s_sceneManager; } }
    public static SoundManager Sound {  get { Init(); return s_soundManager; } }
    public static CharacterManager Character { get { Init(); return s_characterManager; }}
    public static SkillManager Skill { get { Init(); return s_skillManager; } }
    public static UserInfoManager UserInfo { get {Init(); return s_userInfoManager; }}
    public static UIAnimationManager UIAnimation { get { Init(); return s_uiAnimationManager; } }
    
    public static EnemyManager Enemy { get { Init(); return enemyManager; } }
    public static StageManager Stage { get { Init(); return stageManager; } }
    public static InstructionManager Instruction {get {Init(); return instructionManager;}}
    public static StatManager Stat { get { Init(); return statManager; } }
    public static NetworkManager Network { get { Init(); return networkManager; } }
    public static SkillUpgradeManager SkillUpgrade { get { Init(); return skillUpgradeManager; } }
    
    
    
    
    private void Awake() 
    {
        Init();
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
        }
    }

    public static void SceneInit()
    {
        switch (s_sceneManager.CurrentSceneType)
        {
            case Define.Scene.Auth:
                if(NetworkManager.Instance == null)
                    networkManager = InstantiateMonoBehaviourManager<NetworkManager>();
                break;
            case Define.Scene.Lobby:
                if(StatManager.Instance == null)
                    statManager = InstantiateMonoBehaviourManager<StatManager>();
                if(NetworkManager.Instance == null)
                    networkManager = InstantiateMonoBehaviourManager<NetworkManager>();
                if(SkillUpgradeManager.Instance == null)
                    skillUpgradeManager = InstantiateMonoBehaviourManager<SkillUpgradeManager>();
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
