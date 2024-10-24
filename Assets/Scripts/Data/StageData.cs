
using UnityEngine;

[CreateAssetMenu(fileName = "New Stage Data", menuName = "Stage Data")]
public class StageData : ScriptableObject
{
    public static int MAX_LEVEL = 10;
    public LevelData[] levels = new LevelData[MAX_LEVEL];
}