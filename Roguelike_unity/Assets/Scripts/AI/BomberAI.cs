using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberAI : MonoBehaviour
{
    public NPCStatus MyStatus;
    public HomingAI MyNav;
    public Animator MyAnim;
    public float AtkRange;
    public int AtkSfxIndex;
    public float ExplodeDelay = 90;
    float fuse = 600;
    float forceFuse;
    bool fuseSet = false;
    public GameObject ExplosionSpawn;

    private void Start()
    {

        GetComponent<HomingAI>().SetAtkRange(AtkRange);
        MyNav.Relentless(true);
    }

    public int MyDmg = 1;

    void Update()
    {
        if (MyNav.InAtkRange && MyStatus.Alive)
        {
            if(MyNav.FreeMove && !fuseSet)
            {
                MyAnim.SetTrigger("Attack");
                MyNav.PauseMovement(true);
                fuseSet = true;
            }
        }

        if(fuse>0)
        {
            
            fuse -= 60 * Time.deltaTime * GameInfo.GM.GameSpeed;            
            if(fuse<=0 && !fuseSet)
            {
                fuseSet = true;
                MyNav.PauseMovement(true);
                MyAnim.SetTrigger("Attack");
            }
            
        }
    }

    public void CreateExplosion(bool _player, bool _enemy)
    {
        AreaDamage AE = Instantiate(ExplosionSpawn, transform.position, transform.rotation).GetComponent<AreaDamage>();
        AE.Dmg = MyStatus.AtkDmg;
        AE.HitPlayer = _player;
        AE.HitNPC = _enemy;
        

    }

}

