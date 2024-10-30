using System;
using UnityEngine;

public class FloorSwitcher : MonoBehaviour
{
    public GameObject blurTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("In");
            blurTarget.gameObject.SetActive(false);
        }
    }
}
