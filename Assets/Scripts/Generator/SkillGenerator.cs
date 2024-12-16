using System;
using UnityEditor;
using UnityEngine;

public class SkillGenerator : EditorWindow
{
    public int id;
    public string name;
    public Define.SkillType type;
    public int amount;
    public int cost;
    public float range;
    public float duration;
    public float delay;
    public bool vfxOnDelay = false;
    public float coolTime;
    public ParticleSystem vfx;
    public Sprite skillIcon;
    public string description;
    public bool isEquipped;

    [MenuItem("Window/Skill Generator")]
    public static void ShowWindow()
    {
        GetWindow<SkillGenerator>("Skill Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Skill Properties", EditorStyles.boldLabel);
        
        name = EditorGUILayout.TextField("Name", name);
        type = (Define.SkillType)EditorGUILayout.EnumPopup(type);
        amount = EditorGUILayout.IntField("Amount", amount);
        cost  = EditorGUILayout.IntField("Cost", cost);
        range = EditorGUILayout.FloatField("Range", range);
        duration = EditorGUILayout.FloatField("Duration", duration);
        delay = EditorGUILayout.FloatField("Delay", delay);
        vfxOnDelay = EditorGUILayout.Toggle("VFX On Delay", vfxOnDelay);
        coolTime = EditorGUILayout.FloatField("Cool Time", coolTime);
        vfx = EditorGUILayout.ObjectField("VFX", vfx, typeof(ParticleSystem), false) as ParticleSystem;
        skillIcon = (Sprite)EditorGUILayout.ObjectField("Skill Icon", skillIcon, typeof(Sprite), false);
        description = EditorGUILayout.TextField("Description", description);

        if (GUILayout.Button("Generate"))
        {
            GenerateSkill();
        }
    }

    private void GenerateSkill()
    {
        string folderPath = "Assets/Data/Skills";

        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder("Assets", "Data/Skills");
        }
        
        SkillData newSkill = ScriptableObject.CreateInstance<SkillData>();

        newSkill.name = name;
        newSkill.type = this.type;
        newSkill.amount = amount;
        newSkill.cost = cost;
        newSkill.range = range;
        newSkill.duration = duration;
        newSkill.delay = delay;
        newSkill.vfxOnDelay = vfxOnDelay;
        newSkill.coolTime = coolTime;
        newSkill.vfx = vfx;
        newSkill.description = description;
        newSkill.isEquipped = isEquipped;
        newSkill.skillIcon = skillIcon;

        string assetPath = $"{folderPath}/{name}.asset";
        AssetDatabase.CreateAsset(newSkill, assetPath);
        AssetDatabase.SaveAssets();
        
        EditorUtility.DisplayDialog("Skill Generator", $"Skill {name} generated at {assetPath}", "OK");
        
    }
}
