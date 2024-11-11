using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    [SerializeField] private Image hpImage;
    [SerializeField] private Image mpImage;
    
    private void Start()
    {
        Managers.Stage.cc.onStatusChanged += UpdateStatus;
    }

    private void UpdateStatus(CharacterController cc)
    {
        hpImage.fillAmount = cc.hp / cc.maxHp;
        mpImage.fillAmount = cc.mp / cc.maxMp;
    }
}
