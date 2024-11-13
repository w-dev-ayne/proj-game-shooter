using TMPro;
using UnityEngine;

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

    private void InstructionOff()
    {
        currentInstruction = null;
        instructionObj.SetActive(false);
    }
}
