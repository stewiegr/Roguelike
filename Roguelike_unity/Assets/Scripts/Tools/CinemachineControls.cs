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

    public Color32 Fade;
    public Color32 Norm;
    public SpriteRenderer Fader;
    public bool dead = false;
    public GameObject GameArea;

    float ShakeDur = 0;
    float pauseFollow = 0;

    // Start is called before the first frame update
    void Start()
    {
        CCam = GetComponent<CinemachineVirtualCamera>();
        CamID.CMController = this;
        noise = CCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        GameInfo.ZoomOffset = ZoomOffset.transform;
        CamID.Cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShakeDur > 0)
        {
            ShakeDur -= 60 * Time.deltaTime;
            if (ShakeDur <= 0)
            {
                noise.m_AmplitudeGain = 0;
            }
        }

        if (pauseFollow <= 0)
        {
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
        else
        {
            pauseFollow -= 60 * Time.deltaTime;
            CCam.Follow = null;
        }


    }

    public void PauseFollow(float _frames)
    {
        pauseFollow = _frames;
    }

    private void FixedUpdate()
    {
        if (!dead)
        {
            if (!GameArea.activeSelf)
                GameArea.SetActive(true);

            if (zoom)
                DoZoomIn();
            else
                DoZoomOut();
        }
        else
        {
            GameArea.SetActive(false);
            DoDead();
        }
    }

    void DoDead()
    {
        if (PPCam.assetsPPU < 32)
        {
            PPCam.assetsPPU += 1;
        }
        if (PPCam.assetsPPU > 32)
        {
            PPCam.assetsPPU -= 1;
        }
        if (Fader.color != Fade)
            Fader.color = Color32.Lerp(Fader.color, Fade, .1f);
    }

    void DoZoomIn()
    {
        if (PPCam.assetsPPU < 32)
        {
            PPCam.assetsPPU += 1;
        }
        if (PPCam.assetsPPU > 32)
        {
            PPCam.assetsPPU -= 1;
        }
        //if (Fader.color != Fade)
        //     Fader.color = Color32.Lerp(Fader.color, Fade, .1f);
    }

    void DoZoomOut()
    {
        if (PPCam.assetsPPU > 16)
        {
            PPCam.assetsPPU -= 1;
        }
        if (PPCam.assetsPPU < 16)
        {
            PPCam.assetsPPU += 1;
        }
        if (Fader.color != Norm)
            Fader.color = Color32.Lerp(Fader.color, Norm, .1f);
    }

    public void ShakeScreen(float _amp, float _dur)
    {
        ShakeDur = _dur;
        noise.m_AmplitudeGain = _amp;
    }
}
