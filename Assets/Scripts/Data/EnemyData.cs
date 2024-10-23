
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public float hp = 10f;
    public float attack = 1f;
    public float attackSpeed = 2.0f;
    public float moveSpeed = 1.0f;
}