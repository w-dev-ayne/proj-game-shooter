using System;
using UnityEngine;

public class FloorSwitcher : MonoBehaviour
{
    [SerializeField] private int upFloor = 3;
    
    private Camera camera;
    private int currentFloor = 3;
    private float enterDirection = 0;
    private float exitDirection = 0;
    
    public enum Direction{
        x,
        z
    }

    public Direction direction;
    
    //other.GetComponent<Rigidbody>().velocity;

    void Awake()
    {
        camera = Camera.main;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enterDirection = (direction == Direction.x) ? other.transform.forward.x : other.transform.forward.z;
            
            Debug.Log(enterDirection);
            // SwitchCameraCullingMask();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            exitDirection = (direction == Direction.x) ? other.transform.forward.x : other.transform.forward.z;

            if ((enterDirection < 0) == (exitDirection < 0))
            {
                SwitchCameraCullingMask();
            }
            
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
