using System;
using System.Threading.Tasks;
using Unity.VisualScripting;
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
    public int duration;
    public float delay;
    public bool vfxOnDelay = false;
    public float coolTime;
    public ParticleSystem vfx;
    public Sprite skillIcon;
    public string description;
    public bool isEquipped;
    public string sound;

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
        duration = EditorGUILayout.IntField("Duration", duration);
        delay = EditorGUILayout.FloatField("Delay", delay);
        vfxOnDelay = EditorGUILayout.Toggle("VFX On Delay", vfxOnDelay);
        coolTime = EditorGUILayout.FloatField("Cool Time", coolTime);
        vfx = EditorGUILayout.ObjectField("VFX", vfx, typeof(ParticleSystem), false) as ParticleSystem;
        skillIcon = (Sprite)EditorGUILayout.ObjectField("Skill Icon", skillIcon, typeof(Sprite), false);
        description = EditorGUILayout.TextField("Description", description);
        sound = EditorGUILayout.TextField("Sound", sound);

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
        newSkill.cooltime = coolTime;
        newSkill.vfx = vfx;
        newSkill.description = description;
        newSkill.isEquipped = isEquipped;
        newSkill.skillIcon = skillIcon;
        newSkill.sound = sound;

        SkillNetworkData newSkillData = new SkillNetworkData();
        newSkillData.FetchData(newSkill);

        SkillApiService apiService = new SkillApiService();
        await apiService.AddSkill(newSkillData, config);
    }
}
