using CartoonFX;
using UnityEngine;

public class BulletPool : ObjectPool
{
    public CharacterController cc;
    public Transform shootPositionTransform;
    public CameraShake cameraShake;

    protected override void Awake()
    {
        base.Awake();

        cameraShake = this.gameObject.AddComponent<CameraShake>();
    }

    public void CameraShake()
    {
        cameraShake.ShakeCamera();
    }
    

    void Start()
    {
    }
}