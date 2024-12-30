using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatButton : MonoBehaviour
{
    private enum ButtonType
    {
        Character,
        Skill
    }
    
    [SerializeField]
    private ButtonType buttonType = ButtonType.Character;
    
    [SerializeField]
    private TextMeshProUGUI statNameText;
    [SerializeField]
    private TextMeshProUGUI statValueText;
    [SerializeField] 
    private TextMeshProUGUI upAmountText;
    [SerializeField]
    private Button upButton;
    

    public string statName;
    private float initValue;
    public float upAmount { get; private set; }
    
    public void Init(string statName, object initValue)
    {
        this.initValue = (float)initValue;
        this.statName = statName;
        
        switch (this.buttonType)
        {
            case ButtonType.Character:
                this.upAmount = (float)Managers.Character.config.GetType().GetField(statName).GetValue(Managers.Character.config);
                break;
            case ButtonType.Skill:
                this.upAmount = (float)Managers.Skill.config.GetType().GetField(statName).GetValue(Managers.Skill.config);
                break;
        }
        
        
        this.statNameText.text = this.statName;
        this.statValueText.text = ((float)initValue).ToString();
        this.upAmountText.text = upAmount.ToString();
        
        upButton.onClick.AddListener(OnClickUpButton);
        statValueText.color = Managers.Stat.defaultColor;

        Managers.Stat.onPointZero += OnPointZero;
        Managers.Stat.onPointOne += OnPointOne;
        
    }

    private void OnClickUpButton()
    {
        switch (this.buttonType)
        {
            case ButtonType.Character:
                Managers.Stat.PushCommand(this);
                break;
            case ButtonType.Skill:
                Managers.SkillUpgrade.PushCommand(this);
                break;
        }
    }

    public void Do()
    {
        float currentValue = float.Parse(statValueText.text);
        statValueText.text = (currentValue + upAmount).ToString();
        ChangeColor();
    }

    public void UnDo()
    {
        float currentValue = float.Parse(statValueText.text);
        statValueText.text = (currentValue - upAmount).ToString();
        ChangeColor();
    }

    private void OnPointZero()
    {
        upButton.gameObject.SetActive(false);
    }

    private void OnPointOne()
    {
        upButton.gameObject.SetActive(true);
    }

    private void ChangeColor()
    {
        if (float.Parse(statValueText.text) == initValue)
        {
            this.statValueText.color = Managers.Stat.defaultColor;
        }
        else
        {
            this.statValueText.color = Managers.Stat.updatedColor;
        }
    }
}
