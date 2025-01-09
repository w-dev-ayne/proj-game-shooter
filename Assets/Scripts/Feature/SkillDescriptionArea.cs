using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillDescriptionArea : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI type;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI amount;       
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private TextMeshProUGUI range;
    [SerializeField] private TextMeshProUGUI duration;
    [SerializeField] private TextMeshProUGUI delay;
    [SerializeField] private TextMeshProUGUI cooltime;

    public void Initialize(SkillData skill)
    {
        icon.sprite = skill.skillIcon;
        name.text = skill.name;
        type.text = skill.type.ToString();
        description.text = skill.description;
        amount.text = skill.amount.ToString();
        cost.text = skill.cost.ToString();
        range.text = skill.range.ToString();
        duration.text = skill.duration.ToString();
        delay.text = skill.delay.ToString();
        cooltime.text = skill.cooltime.ToString();
    }
}
