using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;


public class DebugTools : MonoBehaviour
{

    public Transform Enemies;
    public TextMeshProUGUI Ct;
    public TextMeshProUGUI FPS;
    public TextMeshProUGUI Res;
    public TextMeshProUGUI Wind;
    int ECt;
    public float deltaTime;
    float FPSupd = 60;
    public List<Resolution> ActiveRes = new List<Resolution>();
    int index = 0;

    private void Start()
    {
        Resolution[] Resolutions = Screen.resolutions;

        foreach(Resolution res in Resolutions)
        {
            if (!ActiveRes.Contains(res))
                ActiveRes.Add(res);
        }
        foreach(Resolution res in ActiveRes)
        {
            if(Screen.currentResolution.width==res.width && Screen.currentResolution.height==res.height)
            {
                break;
            }
            index++;
        }

    }
    // Update is called once per frame
 

    private void Update()
    {
        if (FPSupd > 0)
        {
            FPSupd -= 60 * Time.deltaTime;

            if (FPSupd <= 0)
            {
                FPSupd = 60;
                deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
                float fps = 1.0f / deltaTime;
                FPS.text = "FPS: " + Mathf.Ceil(fps).ToString();
            }
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            if (Screen.fullScreenMode != FullScreenMode.Windowed)
                Screen.fullScreenMode = FullScreenMode.Windowed;
            else
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        if(Input.GetKeyDown(KeyCode.F2))
        {          
            if (index + 1 < ActiveRes.Count)
            {
                while(ActiveRes[index+1].width==Screen.currentResolution.width && ActiveRes[index+1].height==Screen.currentResolution.height)
                {
                    if (index + 1 < ActiveRes.Count)
                        index++;
                    else
                        index = -1;
                }


                index++;
                Screen.SetResolution(ActiveRes[index].width, ActiveRes[index].height, Screen.fullScreen, ActiveRes[index].refreshRate);               
            }
            else
            {
                index = 0;
                Screen.SetResolution(ActiveRes[index].width, ActiveRes[index].height, Screen.fullScreen);
            }
        }

        if(Screen.fullScreen)
        Res.text = "(F2) Res: " + Screen.currentResolution.width + " x " + Screen.currentResolution.height + " x " + Screen.currentResolution.refreshRate + "hz" ;
        else
            Res.text = "(F2) Res: " + Screen.width + " x " + Screen.height + " x " + Screen.currentResolution.refreshRate + "hz";
        Wind.text = "(F3) Full Screen: " + Screen.fullScreenMode.ToString();
    }
}
