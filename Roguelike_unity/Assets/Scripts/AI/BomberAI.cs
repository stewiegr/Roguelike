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
    float fuse = 0;
    float forceFuse;
    public GameObject ExplosionSpawn;

    private void Start()
    {
        forceFuse = ExplodeDelay * 6;
        GetComponent<HomingAI>().SetAtkRange(AtkRange);
        MyNav.Relentless(true);
    }

    public int MyDmg = 1;

    void Update()
    {
        if (MyNav.InAtkRange && MyStatus.Alive)
        {
            if(MyNav.FreeMove)
            {
                fuse = ExplodeDelay;
            }
        }
        if(forceFuse>0)
        {
            forceFuse -= 60 * Time.deltaTime * GameInfo.GM.GameSpeed;
            if (forceFuse <= 0)
                fuse = ExplodeDelay;
        }

        if(fuse>0)
        {
            MyNav.PauseMovement(true);
            fuse -= 60 * Time.deltaTime * GameInfo.GM.GameSpeed; ;
            MyAnim.SetTrigger("Attack");
            if(fuse<=0)
            {
                CreateExplosion(true, true);
                GameObject.Destroy(this.gameObject);
            }
        }
    }

    void CreateExplosion(bool _player, bool _enemy)
    {
        AreaDamage AE = Instantiate(ExplosionSpawn, transform.position, transform.rotation).GetComponent<AreaDamage>();
        AE.Dmg = MyStatus.AtkDmg;
        AE.HitPlayer = _player;
        AE.HitNPC = _enemy;

    }

}

