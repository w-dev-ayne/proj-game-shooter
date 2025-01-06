using System.Collections;
using Unity.Cinemachine;
using Unity.Cinemachine.TargetTracking;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public CharacterController cc;
    public CinemachineCamera followCamera;
    public Transform cameraOffsets;
    public Transform cameraOffset;

    public Joystick moveJoystick;
    public Joystick attackJoystick;

    void Awake()
    {
        StartCoroutine(InitMoveCamera());
    }
    
    private IEnumerator InitMoveCamera()
    {
        LockJoystick(true);
        SetCameraFollowTarget(cameraOffset, new Vector3(0, 1, 0));
        
        float term = 1.5f;
        float timer = 0;
        WaitForFixedUpdate frame = new WaitForFixedUpdate();
        Vector3 dir = Vector3.zero;

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

        SetCameraFollowTarget(cc.transform, new Vector3(1, 1, 1));
        LockJoystick(false);
    }

    public void SetCameraFollowTarget(Transform target, Vector3 damping)
    {
        followCamera.Follow = target;
        followCamera.GetComponent<CinemachineFollow>().TrackerSettings.PositionDamping = damping;
    }

    public void LockJoystick(bool isLock)
    {
        moveJoystick.isLock = isLock;
        attackJoystick.isLock = isLock;
    }
}
