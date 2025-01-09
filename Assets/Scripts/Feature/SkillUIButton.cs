using UnityEngine;
using UnityEngine.UI;

public class SkillUIButton : MonoBehaviour
{
    public SkillData skillData;

    [SerializeField] private Image icon;
    [SerializeField] public GameObject arrow;

    void Awake()
    {
        Initialize();
    }
    
    public void Initialize()
    {
        this.icon.sprite = skillData.skillIcon;
        this.GetComponent<Button>().onClick.AddListener(() =>
        {
            Managers.UI.FindPopup<UI_SkillManagement>().OnClickSkillButton(this);
        });
    }
}
