using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vc;
    [SerializeField] CinemachineBasicMultiChannelPerlin noise;
    private void Awake()
    {
        vc = GetComponent<CinemachineVirtualCamera>();
        Rocket.OnCameraShake += ShakeCamera;
    }
    void Start()
    {
        noise = vc.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    void StopCameraShake()
    {
        noise.m_AmplitudeGain = 0f;
        noise.m_FrequencyGain = 0f;
        
    }

    public void ShakeCamera()
    {
        noise.m_AmplitudeGain = 5f;
        noise.m_FrequencyGain = 3f;
        Invoke("StopCameraShake", 0.3f);
    }
}
