using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.U2D;

public class CinemachineControls : MonoBehaviour
{
    CinemachineBasicMultiChannelPerlin noise;
    CinemachineVirtualCamera CCam;
    public PixelPerfectCamera PPCam;
    public bool zoom = false;
    public GameObject ZoomOffset;
    public GameObject NormalTarg;

    float ShakeDur = 0;
    
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
                noise.m_AmplitudeGain = 0;
            }
        }

        if (GameInfo.PlayerInMenu)
        {
            zoom = true;
            CCam.Follow = ZoomOffset.transform;
        }
        else
        {
            zoom = false;
            CCam.Follow = NormalTarg.transform;
        }


    }

    private void FixedUpdate()
    {
        if (zoom)
            DoZoomIn();
        else
            DoZoomOut();
    }

    void DoZoomIn()
    {
        if(PPCam.assetsPPU<32)
        {
            PPCam.assetsPPU += 1;
        }
    }

    void DoZoomOut()
    {
        if (PPCam.assetsPPU > 16)
        {
            PPCam.assetsPPU -= 1;
        }
    }

    public void ShakeScreen(float _amp, float _dur)
    {
        ShakeDur = _dur;
        noise.m_AmplitudeGain = _amp;
    }
}
