using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDisableObject : MonoBehaviour
{
    float activeFrames = 0;
    public void ActivateObj(float _frames)
    {
        activeFrames = _frames;
    }

    // Update is called once per frame
    void Update()
    {
        if (activeFrames <= 0)
        {
            this.gameObject.SetActive(false);
        }
        else
            activeFrames -= 60 * Time.deltaTime;
    }
}
