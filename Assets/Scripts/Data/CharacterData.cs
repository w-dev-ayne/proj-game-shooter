using UnityEngine;

[System.Serializable]
public class CharacterData : PostData
{
    public float hp;
    public float mp;
    public float attack;
    public float moveSpeed;
    public float rotateSpeed;
    public float bulletSpeed;
    public float attackSpeed;

    public void FetchData(CharacterNetworkData networkData)
    {
        hp = networkData.hp;
        mp = networkData.mp;
        attack = networkData.attack;
        moveSpeed = networkData.moveSpeed;
        rotateSpeed = networkData.rotateSpeed;
        bulletSpeed = networkData.bulletSpeed;
        attackSpeed = networkData.attackSpeed;
    }
}

[System.Serializable]
public class CharacterNetworkData
{
    public float hp = 100f;
    public float mp = 100f;
    public float attack = 1f;
    public float moveSpeed = 1.0f;
    public float rotateSpeed = 1.0f;
    public float bulletSpeed = 1.0f;
    public float attackSpeed = 4.0f;
}