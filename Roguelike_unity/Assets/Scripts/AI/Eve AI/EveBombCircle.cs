using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EveBombCircle : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MyExplosion;
    public Color32 RedZoneHurt;
    public Color32 RedZoneWarn;
    public SpriteRenderer spr;
    int done = 0;
    float explode = 65;

    private void Start()
    {
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector2.Distance(Vector3.one * 2, spr.transform.localScale) > .02f)
        {
            spr.transform.localScale = Vector2.Lerp(spr.transform.localScale, Vector3.one * 2, 5 * Time.deltaTime * GameInfo.GM.GameSpeed);
        }
        else if(done==0)
        {
            done++;
            spr.transform.localScale = Vector3.one * 2;
        }
        if(done==1)
        {
            spr.color = Color.Lerp(spr.color, RedZoneHurt, .5f * Time.deltaTime * GameInfo.GM.GameSpeed);
            explode -= 60 * Time.deltaTime * GameInfo.GM.GameSpeed;
        }
        if(explode<=0)
        {
            if (spr.enabled)
            {
                MyExplosion.SetActive(true);
                spr.enabled = false;
            }
        }
        
       
    }

}
