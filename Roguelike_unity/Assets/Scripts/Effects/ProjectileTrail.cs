using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrail : MonoBehaviour
{
    public Color32 newColor;
    public float a = 255;
    public float fadeRate = 32;
    public SpriteRenderer Render;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(a>0)
        {
            a -= fadeRate;
            newColor.a = (byte)a;
            Render.color = newColor;
        }
        if (a <= 0)
            GameObject.Destroy(this.gameObject);
    }
}
