using System.Collections;
using UnityEngine;

public class UI_SkillDraw : UI_Popup
{
    enum Objects
    {
        DrawingVfxObject,
        DrewVfxObject,
        SkillObject
    }

    enum Buttons
    {
        DrawButton,
        RedrawButton,
        CloseButton,
        BackButton
    }

    enum Texts
    {
        SkillDrawRemainText,
        SkillReDrawRemainText,
    }

    enum Images
    {
        SkillIconImage
    }
    
    public override bool Init()
    {
        BindObject(typeof(Objects));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));
        
        GetButton((int)Buttons.DrawButton).gameObject.BindEvent(OnClickDrawButton);
        GetButton((int)Buttons.RedrawButton).gameObject.BindEvent(OnClickRedrawButton);
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnClickCloseButton);
        GetButton((int)Buttons.BackButton).gameObject.BindEvent(ClosePopupUI);
        
        GetButton((int)Buttons.RedrawButton).gameObject.SetActive(false);
        GetButton((int)Buttons.CloseButton).gameObject.SetActive(false);
        
        GetObject((int)Objects.SkillObject).SetActive(false);
        GetObject((int)Objects.DrawingVfxObject).SetActive(false);
        GetObject((int)Objects.DrewVfxObject).SetActive(false);

        GetText((int)Texts.SkillDrawRemainText).text = Managers.UserInfo.data.skilldrawPoint.ToString();
        
        if (!base.Init())
            return false;
        return true;
    }

    private void OnClickDrawButton()
    {
        GetButton((int)Buttons.DrawButton).gameObject.SetActive(false);
        
        StopAllCoroutines();
        StartCoroutine(CoDrawSkill());
    }

    private IEnumerator CoDrawSkill()
    {
        Managers.Skill.DrawSkill();
        GetObject((int)Objects.DrewVfxObject).SetActive(false);
        GetObject((int)Objects.DrawingVfxObject).SetActive(true);
        yield return new WaitForSeconds(2.5f);
        GetObject((int)Objects.DrawingVfxObject).SetActive(false);
        GetObject((int)Objects.DrewVfxObject).SetActive(true);
        
        OnDrewSkill();
    }

    private void OnDrewSkill()
    {
        GetButton((int)Buttons.RedrawButton).gameObject.SetActive(true);
        GetButton((int)Buttons.CloseButton).gameObject.SetActive(true);
        GetObject((int)Objects.SkillObject).SetActive(true);
    }

    public void SetDrewSkillObject(SkillData skillData)
    {
        GetImage((int)Images.SkillIconImage).sprite = skillData.skillIcon;
    }

    private void OnClickRedrawButton()
    {
        GetObject((int)Objects.SkillObject).SetActive(false);
        
        GetButton((int)Buttons.RedrawButton).gameObject.SetActive(false);
        GetButton((int)Buttons.CloseButton).gameObject.SetActive(false);
        
        StopAllCoroutines();
        StartCoroutine(CoDrawSkill());
    }

    private void OnClickCloseButton()
    {
        ClosePopupUI();
    }

    public void SetSkillDrawRemainText()
    {
        GetText((int)Texts.SkillReDrawRemainText).text = Managers.UserInfo.data.skilldrawPoint.ToString();
        GetText((int)Texts.SkillDrawRemainText).text = Managers.UserInfo.data.skilldrawPoint.ToString();
    }
}
