using System;
using UnityEngine;

public class FloorSwitcher : MonoBehaviour
{
    [SerializeField] private int upFloor = 3;
    
    private Camera camera;
    private int currentFloor = 3;
    private Vector3 enterDirection = Vector3.zero; 
    
    //other.GetComponent<Rigidbody>().velocity;

    void Awake()
    {
        camera = Camera.main;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (enterDirection != Vector3.zero && Vector3.Dot(enterDirection, other.transform.forward) > 0)
            {
                return;
            }
            enterDirection = other.GetComponent<Rigidbody>().linearVelocity;
            SwitchCameraCullingMask();
        }
    }

    
    private void SwitchCameraCullingMask()
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
