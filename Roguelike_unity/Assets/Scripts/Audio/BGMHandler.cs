using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMHandler : MonoBehaviour
{
    public AudioControl AudControl;
    public AudioSource Mix1;
    public List<AudioClip> ActiveMix;
    public List<AudioClip> ForestBGM;
    AudioClip QueueTrack = null;

    bool panDown = false;
    bool panUp = false;
    int curIndex = 0;


    public float PlayVol = .1f;
    private void Start() 
    {
        Mix1.volume = 0;
        SetActiveMix(ForestBGM);
        SwitchTrack(ActiveMix, 0);
    }

    private void Update()
    {
        if (panDown)
        {
            if (Mix1.volume > 0)
                Mix1.volume -= .1f * Time.deltaTime;
            else
            {
                SetTrack();
                panDown = false;
            }
        }
        if(panUp)
        {
            if(Mix1.volume < PlayVol)
            {
                Mix1.volume += .2f * Time.deltaTime;
            }
            else
            {
                Mix1.volume = PlayVol;
                panUp = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.T))
        {
            if (curIndex == 0)
                SwitchTrack(ActiveMix, 1);
            else
                SwitchTrack(ActiveMix, 0);
        }

        if(!Mix1.isPlaying && QueueTrack==null)
        {
            if(curIndex < ActiveMix.Count-1)
            {
                SwitchTrack(ActiveMix, curIndex + 1);
            }
            else
            {
                SwitchTrack(ActiveMix, 0);
            }
        }
    }

    public void SwitchTrack(List<AudioClip> _mix, int _index)
    {
        panDown = true;
        panUp = false;
        QueueTrack = _mix[_index];
        curIndex = _index;

    }

    void SetTrack()
    {
        if (QueueTrack != null)
            Mix1.clip = QueueTrack;

        panUp = true;
        if (!AudControl.MuteMusic)
        Mix1.Play();
        QueueTrack = null;
    }

    void SetActiveMix(List<AudioClip> _active)
    {
        ActiveMix = _active;
    }

}
