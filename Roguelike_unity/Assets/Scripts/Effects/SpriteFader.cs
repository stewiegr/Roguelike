using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFader : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer MyRender;
    public AreaDamageOverTime DOT;
    int alpha;
    public Color32 Fader;
    public float TimeToFade;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (TimeToFade <= 0)
        {
            if (DOT != null)
                DOT.enabled = false;
            Fader.a -= 10;
            MyRender.color = Fader;
            if (Fader.a <= 10)
                GameObject.Destroy(this.gameObject);
        }
        else
            TimeToFade--;
    }
}
