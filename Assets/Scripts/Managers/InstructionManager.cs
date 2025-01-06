using TMPro;
using UnityEngine;

// 게임 진행 중, 여러 상황에 대한 지시 사항 Manager
public class InstructionManager : MonoBehaviour
{
    private GameObject instructionObj;
    private TextMeshProUGUI instructionTmp;
    private string currentInstruction;

    public void Init(GameObject obj, TextMeshProUGUI tmp)
    {
        instructionObj = obj;
        instructionTmp = tmp;
    }

    // 지시 사항 보이기
    public void InstructionOn(string instruction)
    {
        if (currentInstruction != null)
        {
            CancelInvoke();
        }
        this.currentInstruction = instruction;
        instructionTmp.text = currentInstruction;
        instructionObj.SetActive(true);
        Invoke("InstructionOff", 1.0f);
    }

    // 지시 사항 끄기
    private void InstructionOff()
    {
        currentInstruction = null;
        instructionObj.SetActive(false);
    }
}
