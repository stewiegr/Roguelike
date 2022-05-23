using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashFade : MonoBehaviour
{
    public SpriteRenderer MyRend;
    public Color FlashScr;
    public Color Transp;

    // Update is called once per frame
    void Update()
    {
        if(MyRend.color.a > 0)
        {
            MyRend.color = Color.Lerp(MyRend.color, Transp, .01f);
        }
    }
}
