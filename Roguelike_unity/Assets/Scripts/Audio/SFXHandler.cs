using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip[] SFX;
    public AudioSource Aud;
    public AudioControl AudControl;

    void Start()
    {
        GameInfo.SFX = this;
    }

    public void PlayAudioClip(int index)
    {
        if(!AudControl.MuteSFX)
        Aud.PlayOneShot(SFX[index]);
    }
    
}
