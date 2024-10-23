using UnityEngine;

public class Followable : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private bool position;

    private Vector3 offset;

    private void Start()
    {
        if(target != null)
            offset = transform.position - target.position;
    }

    private void Update()
    {
        if (target == null)
            return;
        
        if (position)
        {
            transform.position = target.position + offset;
        }
    }
}
