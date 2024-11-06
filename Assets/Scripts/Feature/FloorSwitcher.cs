using System;
using UnityEngine;

public class FloorSwitcher : MonoBehaviour
{
    [SerializeField] private int upFloor = 3;
    
    private Camera camera;
    private int currentFloor = 3;

    void Awake()
    {
        camera = Camera.main;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        SwitchCameraCullingMask(other);
    }


    private void SwitchCameraCullingMask(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            currentFloor = (currentFloor == 3) ? 2 : 3;
            if (currentFloor == 2)
            {
                camera.cullingMask = ~((1 << 6) | (1 << 11));    
            }
            else if (currentFloor == 3)
            {
                camera.cullingMask = ~(1 << 6);
            }
        }
    }
}
