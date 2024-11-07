
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public GameObject prefab;
    public float hp = 10f;
    public float attack = 1f;
    public float attackSpeed = 2.0f;
    public float moveSpeed = 1.0f;
    public float attackRange = 1.0f;
    public float bulletSpeed = 2.0f;
}