using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    GameManager GM;
    int loadLevel = -1;
    float loadDel = 0;
    bool FadeOutMenu = false;
    bool whitenBG = false;
    public bool fadeBG = false;
    int savedLevel = 0;
    public GameObject Buttons;
    public Image BlackBG;
    public Color fadeColor;
    public Color White;
    public Color Black;
    public Image MenuImg;
    public GameObject CanvasObj;
    float dur = 5;
    float t = 0;

    private void Start()
    {
        GM = GameInfo.GM;
    }
    // Update is called once per frame
    void Update()
    {
        if (CanvasObj.activeSelf && FadeOutMenu)
        {
            FadeIntoGame();
        }
    }

    public void OpenMenu()
    {
        Buttons.SetActive(true);
        CanvasObj.SetActive(true);
        MenuImg.color = White;
        BlackBG.color = Black;
    }

    public void StartGame()
    {
        loadLevel = savedLevel;
        FadeOutMenu = true;
        Buttons.SetActive(false);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void FadeIntoGame()
    {
        if (loadDel > 0)
        {
            loadDel -= 60 * Time.deltaTime;
            if (loadDel <= 0)
            {
                if (loadLevel != -1)
                    GameInfo.GM.BeginLevel(loadLevel);
            }
        }
        if (FadeOutMenu && !fadeBG)
        {
            if (MenuImg.color != fadeColor)
            {
                MenuImg.color = Color.Lerp(MenuImg.color, fadeColor, Time.deltaTime * 5);
            }
            else
            {
                fadeBG = true;
                GM.BeginLevel(loadLevel);
            }
        }

        if (fadeBG == true)
        {
            if (BlackBG.color != fadeColor)
            {
                BlackBG.color = Color.Lerp(BlackBG.color, fadeColor, Time.deltaTime * 5);
            }
            else
            {
                FadeOutMenu = false;
                whitenBG = false;
                fadeBG = false;
                CanvasObj.SetActive(false);
            }

        }
    }
}
