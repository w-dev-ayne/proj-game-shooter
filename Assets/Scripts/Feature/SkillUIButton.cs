using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIButton : MonoBehaviour
{
    public SkillData skillData;

    [SerializeField] private Image icon;
    
    public void Initialize()
    {
        this.icon.sprite = skillData.skillIcon;
    }
}
