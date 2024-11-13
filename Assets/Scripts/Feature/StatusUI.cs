using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    [SerializeField] private CharacterController cc;
    [SerializeField] private Image hpImage;
    [SerializeField] private Image mpImage;
    
    private void Start()
    {
        if (cc == null)
            this.transform.parent.GetComponent<CharacterController>();
        
        cc.onStatusChanged += UpdateStatus;
    }

    private void UpdateStatus(CharacterController cc)
    {
        hpImage.fillAmount = cc.hp / cc.maxHp;
        mpImage.fillAmount = cc.mp / cc.maxMp;
    }
}
