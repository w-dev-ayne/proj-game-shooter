using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 자식 오브젝트 중, InputField 오브젝트들 간에 Tab, Shift+Tab 전환
public class TabNavigation : MonoBehaviour
{
    private TMP_InputField[] inputFields;

    private void OnEnable()
    {
        this.inputFields = this.transform.GetComponentsInChildren<TMP_InputField>();
    }

    void Update()
    {
        // Tab 키가 눌렸을 때 실행
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // 현재 포커스가 있는 InputField를 찾습니다.
            GameObject currentSelected = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

            // 현재 선택된 InputField가 배열에 있는지 확인
            int index = System.Array.IndexOf(inputFields, currentSelected.GetComponent<TMP_InputField>());

            if (index >= 0)
            {
                // Shift 키가 눌려 있으면 이전 InputField로 이동
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    index = (index - 1 + inputFields.Length) % inputFields.Length;
                }
                else // 그렇지 않으면 다음 InputField로 이동
                {
                    index = (index + 1) % inputFields.Length;
                }

                // 다음 InputField로 포커스를 이동
                inputFields[index].Select();
            }
        }
    }
}