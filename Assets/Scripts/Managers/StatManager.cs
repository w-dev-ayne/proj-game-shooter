using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Reflection;
using UnityEngine.Events;

public class StatManager : Singleton<StatManager>
{
    [SerializeField]
    private CharacterData cData;

    private int initPoint = 0;
    public int currentPoint { get; private set; }

    [Header("Stat Control UI")]
    [SerializeField] public Color defaultColor;
    [SerializeField] public Color updatedColor;
    [SerializeField] public GameObject statButtonPrefab;
    [SerializeField] private Transform statButtonsParent;

    private Stack<StatButton> commands = new Stack<StatButton>();
    public UnityAction<int> onPointChange;
    public UnityAction onPointZero;
    public UnityAction onPointOne;

    public void Init()
    {
        initPoint = Managers.UserInfo.data.characterPoint;
        currentPoint = initPoint;
        onPointChange.Invoke(currentPoint);
        InstantiateStatButtons();
    }

    private void InstantiateStatButtons()
    {
        statButtonsParent = Managers.UI.FindPopup<UI_Stat>().GetCharacterInfoObject();
        FieldInfo[] fields = cData.GetType().GetFields();

        for (int i = 0; i < fields.Length; i++)
        {
            FieldInfo field = fields[i];
            StatButton button = Instantiate(statButtonPrefab, statButtonsParent).GetComponent<StatButton>();
            button.Init(field.Name, field.GetValue(cData));
        }
    }

    public void PushCommand(StatButton button)
    {
        if (currentPoint == 0)
            return;
        
        commands.Push(button);
        button.Do();
        currentPoint--;
        onPointChange.Invoke(currentPoint);

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
        CharacterUpgradeNetworkData data = new CharacterUpgradeNetworkData();
        while (commands.Count > 0)
        {
            StatButton button = commands.Pop();
            FieldInfo field = data.GetType().GetField(button.statName); 
            int updateValue = (int)field.GetValue(data) + 1;
            field.SetValue(data, updateValue);
        }
        currentPoint = 0;
        Managers.Character.UpgradeData(data);
    }

    public void SetCurrentPoint(int stat)
    {
        this.currentPoint = stat;
    }
}
