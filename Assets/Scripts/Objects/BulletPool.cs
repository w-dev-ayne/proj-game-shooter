using UnityEngine;

public class BulletPool : ObjectPool
{
    public CharacterController cc;
    public Transform shootPositionTransform;

    void Start()
    {
        foreach (Bullet bullet in pool)
        {
            bullet.attack = cc.attack;
        }
    }
}