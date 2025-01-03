using System.Collections;
using Unity.Cinemachine;
using Unity.Cinemachine.TargetTracking;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public CinemachineCamera followCamera;
    public CharacterController cc;
    public Transform cameraOffsets;
    public Transform cameraOffset;

    void Awake()
    {
        StartCoroutine(InitMoveCamera());
    }
    
    private IEnumerator InitMoveCamera()
    {
        float term = 1.5f;
        float timer = 0;
        WaitForFixedUpdate frame = new WaitForFixedUpdate();
        Vector3 dir = Vector3.zero;
        followCamera.Follow = cameraOffset;
        followCamera.GetComponent<CinemachineFollow>().TrackerSettings.PositionDamping = new Vector3(0, 1, 0);

        yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < cameraOffsets.childCount; i++)
        {
            if (i == cameraOffsets.childCount - 1)
            {
                dir = cameraOffsets.GetChild(0).position - cameraOffsets.GetChild(i).position;
            }
            else
            {
                dir = cameraOffsets.GetChild(i + 1).position - cameraOffsets.GetChild(i).position;
            }
            
            while (timer <= term)
            {
                cameraOffset.position += dir * Time.deltaTime / term;
                timer += Time.deltaTime;
                yield return frame;
            }

            timer = 0;
        }
        followCamera.GetComponent<CinemachineFollow>().TrackerSettings.PositionDamping = new Vector3(1, 1, 1);
        followCamera.Follow = cc.transform;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
