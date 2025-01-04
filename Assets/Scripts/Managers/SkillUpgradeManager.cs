using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Reflection;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class SkillUpgradeManager : Singleton<SkillUpgradeManager>
{
    public SkillData skillData { get; private set; } = new SkillData();
    public SkillData previousSkillData { get; private set; } = new SkillData();
    private SkillUpgradeData skillUpgradeData = new SkillUpgradeData(); 

    private int initPoint = 0;
    public int currentPoint { get; private set; }

    [Header("Stat Control UI")]
    [SerializeField] public Color defaultColor;
    [SerializeField] public Color updatedColor;
    [SerializeField] public GameObject skillButtonPrefab;
    [SerializeField] private Transform skillButtonsParent;

    private Stack<StatButton> commands = new Stack<StatButton>();
    public UnityAction<int> onPointChange;
    public UnityAction onPointZero;
    public UnityAction onPointOne;

    void Awake()
    {
        base.Awake();
    }

    public void SetSkillData(SkillData skillData)
    {
        previousSkillData = this.skillData;
        this.skillData = skillData;
        skillUpgradeData.FetchData(skillData);
    }

    public void SetSkillDataNull()
    {
        skillData = null;
        skillUpgradeData = null;
    }

    public void Init()
    {
        initPoint = Managers.UserInfo.data.skillPoint;
        currentPoint = initPoint;
        onPointChange?.Invoke(currentPoint);
        InstantiateStatButtons();
    }

    private void InstantiateStatButtons()
    {
        skillButtonsParent = Managers.UI.FindPopup<UI_SkillUpgrade>().GetSkillInfoObject();
        FieldInfo[] fields = skillUpgradeData.GetType().GetFields();

        for (int i = 0; i < fields.Length; i++)
        {
            FieldInfo field = fields[i];
            StatButton button = Instantiate(skillButtonPrefab, skillButtonsParent).GetComponent<StatButton>();
            button.Init(field.Name, field.GetValue(skillUpgradeData));
        }
    }

    public void PushCommand(StatButton button)
    {
        if (currentPoint == 0)
            return;
        
        commands.Push(button);
        button.Do();
        currentPoint--;
        onPointChange?.Invoke(currentPoint);

        if (currentPoint == 0)
        {
            onPointZero?.Invoke();
        }
    }

    public void PopCommand()
    {
        if (currentPoint == initPoint)
            return;
        
        commands.Pop().UnDo();
        currentPoint++;
        onPointChange.Invoke(currentPoint);
        
        if (currentPoint == 1)
        {
            onPointOne?.Invoke();
        }
    }

    public void PopAllCommands()
    {
        while (commands.Count > 0)
        {
            PopCommand();
        }
        onPointOne?.Invoke();
    }

    public void ApplyAllCommands()
    {
        SkillUpgradeNetworkData data = new SkillUpgradeNetworkData();
        data.skillId = skillData.id;
        while (commands.Count > 0)
        {
            StatButton button = commands.Pop();
            FieldInfo field = data.GetType().GetField(button.statName); 
            int updateValue = (int)field.GetValue(data) + 1;
            field.SetValue(data, updateValue);
        }
        currentPoint = 0;
        Managers.Skill.UpgradeSkill(data);
    }

    public void SetCurrentPoint(int stat)
    {
        this.currentPoint = stat;
    }
}
