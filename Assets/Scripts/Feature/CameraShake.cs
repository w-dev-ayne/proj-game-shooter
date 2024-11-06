using CartoonFX;
using Unity.Cinemachine;
using UnityEditor;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CinemachineCamera camera;
    private CinemachineBasicMultiChannelPerlin perlin;
    
    private float shakeDuration = 0.5f;
    private float shakeAmplitude = 1.2f;
    private float shakeFrequency = 2.0f;

    private float shakeElapsedTime = 0f;
    
    void Awake()
    {
        camera = GameObject.FindAnyObjectByType<CinemachineCamera>();
        perlin = camera.GetComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera()
    {
        shakeElapsedTime = this.shakeDuration;
    }

    void Update()
    {
        if (shakeElapsedTime > 0)
        {
            perlin.AmplitudeGain = shakeAmplitude;
            perlin.FrequencyGain = shakeFrequency;
            
            shakeElapsedTime -= Time.deltaTime;
        }
        else
        {
            perlin.AmplitudeGain = 0f;
            perlin.FrequencyGain = 0f;
        }
    }
    
}