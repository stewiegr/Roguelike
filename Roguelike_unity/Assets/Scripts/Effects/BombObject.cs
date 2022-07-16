using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombObject : MonoBehaviour
{
    public DestroyWorldObject DO;
    public SpriteRenderer MyRend;
    float Timer = 300;
    bool flashMe = false;
    bool landed = false;
    float landY;
    float fallSpeed = -1;
    int dmg;


    public void InitBomb(float _landY, int _dmg)
    {
        landed = false;
        landY = _landY;
        dmg = _dmg;
        DO.modDmg = _dmg;
    }

    // Update is called once per frame
    void Update()
    {
        if(Timer>0)
        {
            Timer -= 60 * Time.deltaTime;
            if(Timer<=90)
            {
               if(!flashMe)
                {
                    flashMe = true;
                    GetComponent<Animator>().SetTrigger("Flash");
                }
            }
            if(Timer<=0)
            {
                DO.DestroyMe(Vector3.zero, dmg);
            }


        }
        if(!landed)
        {
            transform.Translate(new Vector3(0, fallSpeed*Time.deltaTime, 0));
            fallSpeed -= .33f;
            if(transform.position.y<=landY)
            {
                fallSpeed = 0;
                landed = true;
                transform.position = new Vector3(transform.position.x, landY, 0);
            }
        }
    }
}
