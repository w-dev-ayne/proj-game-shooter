using System;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class SkillGenerator : EditorWindow
{
    public NetworkConfig config;
    
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

    private async void OnGUI()
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
            await GenerateSkill();
        }
    }

    private async Task GenerateSkill()
    {
        SkillData newSkill = new SkillData();

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

        SkillNetworkData newSkillData = new SkillNetworkData();
        newSkillData.FetchData(newSkill);

        SkillDataController dataController = new SkillDataController();
        await dataController.AddSkill(newSkillData, config);
    }
}
