using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShroomAI : MonoBehaviour
{
    public NPCStatus MyStatus;
    public HomingAI MyNav;
    public Animator MyAnim;

    float atkDly = 30;
    float shieldDly = 0;
    public int MyDmg = 1;
    public float AtkRange = 5;

    public GameObject offspring;
    List<GameObject> Spawned = new List<GameObject>();
    float offspringSpawnTime = 60;

    public int AtkSfxIndex;
    private void Start()
    {
        GetComponent<HomingAI>().SetAtkRange(AtkRange);
    }
    void Update()
    {
        if (MyStatus.AtkDly > 0)
        {
            atkDly -= 60 * Time.deltaTime * GameInfo.GM.GameSpeed;
        }
        if(shieldDly>=0)
        {
            shieldDly -= 60 * Time.deltaTime * GameInfo.GM.GameSpeed;
            if(shieldDly<=0)
            {
                MyAnim.SetBool("Shielding", false);
                MyNav.PauseMovement(false);
                MyStatus.Shielded = false;
                MyDmg = 2;
                atkDly = 400;
            }
        }

        if(offspringSpawnTime>0 && !MyStatus.Shielded && MyNav.InAtkRange)
        {
            offspringSpawnTime -= 60 * Time.deltaTime;
            if(offspringSpawnTime<=0)
            {
                Spawned.Add(Instantiate(offspring, transform.position, transform.rotation));
                offspringSpawnTime = 60;
            }
        }

        if (MyNav.InAtkRange && MyStatus.Alive)
        {
            MyAnim.SetBool("Idle", true);
            if (atkDly <= 0 && shieldDly<=0 && MyNav.FreeMove)
            {
                MyAnim.SetBool("Shielding",true);
                MyNav.PauseMovement(true);
                MyStatus.Shielded = true;
                MyDmg = (int)GameInfo.PlayerStatus.AttackDamage;
                shieldDly = 180;
            }
        }
        else if (!MyNav.InAtkRange)
            MyAnim.SetBool("Idle", false);
    }


}
