using UnityEngine;
using UnityEngine.PlayerLoop;

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

[System.Serializable]
public class CharacterUpgradeNetworkData : PostData
{
    public int hp;
    public int mp;
    public int attack;
    public int moveSpeed;
    public int rotateSpeed;
    public int bulletSpeed;
    public int attackSpeed;
}

public class CharacterUpgradeConfiguration
{
    public float hp;
    public float mp;
    public float attack;
    public float moveSpeed;
    public float rotateSpeed;
    public float bulletSpeed;
    public float attackSpeed;

    public void FetchData(ConfigurationNetworkData[] datas)
    {
        foreach (ConfigurationNetworkData data in datas)
        {
            switch (data.type)
            {
                case "HP":
                    hp = data.amount;
                    break;
                case "MP":
                    mp = data.amount;
                    break;
                case "ATTACK":
                    attack = data.amount;
                    break;
                case "MOVE_SPEED":
                    moveSpeed = data.amount;
                    break;
                case "ROTATE_SPEED":
                    rotateSpeed = data.amount;
                    break;
                case "BULLET_SPEED":
                    bulletSpeed = data.amount;
                    break;
                case "ATTACK_SPEED":
                    attackSpeed = data.amount;
                    break;
            }
        }
    }

    public void Print()
    {
        Debug.Log($"Hp : {hp}");
        Debug.Log($"Mp : {mp}");
        Debug.Log($"Attack : {attack}");
        Debug.Log($"Move Speed : {moveSpeed}");
        Debug.Log($"Rotate Speed : {rotateSpeed}");
        Debug.Log($"Bullet Speed: {bulletSpeed}");
        Debug.Log($"Attack Speed : {attackSpeed}");
    }
}