using System;
using UnityEngine;

public class AutoOff : MonoBehaviour
{
    [SerializeField] private float delay;

    private void OnEnable()
    {
        Invoke("Off", delay);
    }

    private void Off()
    {
        gameObject.SetActive(false);
    }
}
