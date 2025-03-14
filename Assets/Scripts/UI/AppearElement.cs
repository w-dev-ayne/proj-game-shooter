using System;
using DG.Tweening;
using UnityEngine;

public class AppearElement : MonoBehaviour
{
    public Define.AppearDirection direction;

    private Vector3 targetPosition;
    private static float duration = 0.3f;

    private void Awake()
    {
        targetPosition = this.transform.position;

        switch (direction)
        {
            case Define.AppearDirection.left:
                this.transform.position = this.transform.position + new Vector3(-500, 0, 0);
                break;
            case Define.AppearDirection.right:
                this.transform.position = this.transform.position + new Vector3(500, 0, 0);
                break;
            case Define.AppearDirection.top:
                this.transform.position = this.transform.position + new Vector3(0, 300, 0);
                break;
            case Define.AppearDirection.bottom:
                this.transform.position = this.transform.position +  new Vector3(0, -300, 0);
                break;
            default:
                break;
        }
    }

    private void OnEnable()
    {
        Play();
    }

    private void Play()
    {
        this.transform.DOMove(targetPosition, duration, true);
    }
}
