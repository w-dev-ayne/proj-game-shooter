using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Reflection;
using Unity.Android.Types;
using UnityEngine.Events;

public class StatManager : MonoBehaviour
{
    [SerializeField]
    private CharacterData cData;

    private int initPoint = 0;
    public int currentPoint = 5;
    
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
        currentPoint = 5;
        initPoint = currentPoint;
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
        while (commands.Count > 0)
        {
            StatButton button = commands.Pop();
            FieldInfo field = cData.GetType().GetField(button.statName); 
            float updateValue = (float)field.GetValue(cData) + button.upAmount;
            field.SetValue(cData, updateValue);
        }
        currentPoint = 0;
    }
}
