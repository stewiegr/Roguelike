using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : MonoBehaviour
{
    public NPCStatus MyStatus;
    public HomingAI MyNav;
    public Animator MyAnim;
    public float AtkRange;

    private void Start()
    {
        GetComponent<HomingAI>().SetAtkRange(AtkRange);
    }

    float atkDly = 30;
    public int MyDmg = 1;

    void Update()
    {
        if (MyStatus.AtkDly > 0)
        {
            atkDly -= 60 * Time.deltaTime * GameInfo.GM.GameSpeed;
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
