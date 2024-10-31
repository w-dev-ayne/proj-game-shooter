using UnityEngine;

public class RotateTo : MonoBehaviour
{
    public Transform target;

    void Start()
    {
        if (target == null)
        {
            target = Camera.main.transform;
        }
    }
    
    void Update()
    {
        if(target != null)
            transform.LookAt(target);
    }
}
