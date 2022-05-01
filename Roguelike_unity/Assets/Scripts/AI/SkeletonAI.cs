using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAI : MonoBehaviour
{
    public NPCStatus MyStatus;
    public HomingAI MyNav;
    public Animator MyAnim;

    float atkDly = 30;
    public int MyDmg = 1;

    void Update()
    {
        if (MyStatus.AtkDly > 0)
        {
            atkDly -= 60 * Time.deltaTime;
        }
        if (MyNav.InAtkRange && MyStatus.Alive)
        {
            if(atkDly<=0 && MyNav.FreeMove)
            {
                MyAnim.SetTrigger("Attack");
                atkDly = MyStatus.AtkDly;
                GameInfo.PlayerStatus.DamagePlayer(MyDmg);
            }
        }
    }
}
