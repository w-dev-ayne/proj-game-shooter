
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string name;
    public GameObject prefab;
    public float hp = 10f;
    public float attack = 1f;
    public float attackSpeed = 2.0f;
    public float moveSpeed = 1.0f;
    public float attackRange = 1.0f;
    public float bulletSpeed = 2.0f;
    public int gainStat = 0;
}

[System.Serializable]
public class EnemyNetworkData : PostData
{
    public int stageId;
    public int levelId;
    public string name;
    public int modelingId;
    public string attackType;
    public float hp;
    public float attack;
    public float attackSpeed;
    public float moveSpeed;
    public float attackRange;
    public float bulletSpeed;
    public float gainStat;
}