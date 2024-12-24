using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EnterNavigation : MonoBehaviour
{
    public UnityEvent onClickEnter;
    private TMP_InputField[] inputFields;

    void OnEnable()
    {
        this.inputFields = this.transform.GetComponentsInChildren<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // 현재 포커스가 있는 InputField를 찾습니다.
            GameObject currentSelected = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

            int index = -1;
            if (currentSelected != null && currentSelected.TryGetComponent<TMP_InputField>(out TMP_InputField field))
            {
                // 현재 선택된 InputField가 배열에 있는지 확인
                index = System.Array.IndexOf(inputFields, field);
            }
            else
            {
                return;
            }
            
            if (index >= 0)
            {
                onClickEnter?.Invoke();    
            }
        }
    }
}
