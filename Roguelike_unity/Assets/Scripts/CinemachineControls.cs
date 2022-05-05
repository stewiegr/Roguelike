using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineControls : MonoBehaviour
{
    CinemachineBasicMultiChannelPerlin noise;
    CinemachineVirtualCamera CCam;
    float ShakeDur = 0;
    float ShakeMag = 0;
    // Start is called before the first frame update
    void Start()
    {
        CCam = GetComponent<CinemachineVirtualCamera>();
        CamID.CMController = this;
        noise = CCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ShakeDur>0)
        {
            ShakeDur -= 60 * Time.deltaTime;
            if(ShakeDur<=0)
            {
                ShakeMag = 0;
                noise.m_AmplitudeGain = 0;
            }
        }
    }

    public void ShakeScreen(float _amp, float _dur)
    {
        ShakeDur = _dur;
        noise.m_AmplitudeGain = _amp;
    }
}
