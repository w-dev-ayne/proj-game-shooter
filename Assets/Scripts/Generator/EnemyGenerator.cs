using UnityEditor;
using UnityEngine;

public class EnemyGenerator : EditorWindow
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
    
    
    [MenuItem("Window/Enemy Generator")]
    public static void ShowWindow()
    {
        GetWindow<EnemyGenerator>("Enemy Generator");
    }
    
    private void OnGUI()
    {
        GUILayout.Label("Enemy Properties", EditorStyles.boldLabel);
        
        name = EditorGUILayout.TextField("Name", name);
        prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), true);
        hp = EditorGUILayout.FloatField("HP", hp);
        attack = EditorGUILayout.FloatField("Attack", attack);
        attackSpeed = EditorGUILayout.FloatField("Attack Speed", attackSpeed);
        moveSpeed = EditorGUILayout.FloatField("Move Speed", moveSpeed);
        attackRange = EditorGUILayout.FloatField("Attack Range", attackRange);
        bulletSpeed = EditorGUILayout.FloatField("Bullet Speed", bulletSpeed);
        gainStat = EditorGUILayout.IntField("Gain Stat", gainStat);

        if (GUILayout.Button("Generate"))
        {
            GenerateEnemy();
        }
    }

    private void GenerateEnemy()
    {
        string folderPath = "Assets/Data/Enemies";

        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder("Assets", "Data/Enemies");
        }

        EnemyData newEnemy = CreateInstance<EnemyData>();
        
        newEnemy.name = name;
        newEnemy.prefab = prefab;
        newEnemy.hp = hp;
        newEnemy.attack = attack;
        newEnemy.attackSpeed = attackSpeed;
        newEnemy.moveSpeed = moveSpeed;
        newEnemy.attackRange = attackRange;
        newEnemy.bulletSpeed = bulletSpeed;
        newEnemy.gainStat = gainStat;
        

        string assetPath = $"{folderPath}/{name}.asset";
        AssetDatabase.CreateAsset(newEnemy, assetPath);
        AssetDatabase.SaveAssets();
        
        EditorUtility.DisplayDialog("Skill Generator", $"Skill {name} generated at {assetPath}", "OK");
    }
}
