using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EveAI : MonoBehaviour
{
    public NPCStatus MyStatus;
    public EveNavigation MyNav;
    public Animator MyAnim;

    float atkDly = 30;
    public float BigAtkDelay;
    //public float BigAtk2Delay;
    // public float BigAtk3Delay;
    public int MyDmg = 1;
    public float AtkRange = 5;
    public GameObject MyProjectile;
    public Transform LaunchPointPositionR;
    public Transform LaunchPointPositionL;
    Transform LaunchPoint;
    public int AtkSfxIndex;

    public Color RedZoneWarn;
    public Color RedZoneHurt;
    public GameObject RingOfFire;

    bool DoAtk1;
    float Atk1Angle;
    float Atk1ShotDelay = 20;
    int Atk1Stage = 0;
    int Atk1Shots = 0;
    float atk1StageDelay = 0;
    public GameObject Atk1Slash;
    Vector2 TPPos;
    public List<SpriteRenderer> Atk1RedZone;
    public List<Vector2> Atk1RedZoneFullScale = new List<Vector2>();
    public GameObject Bombs;
    public Animator Scythe;
    public GameObject ScytheDamageArea;
    bool Atk1SkullAnim = false;

    bool DoAtk2;
    int Atk2Shots = 0;
    float Atk2Angle;
    float Atk2ShotDelay = 20;


    private void Start()
    {
        foreach (SpriteRenderer spr in Atk1RedZone)
        {
            Atk1RedZoneFullScale.Add(spr.transform.localScale);
            spr.transform.localScale = Vector2.zero;
        }

    }
    void Update()
    {
        DoCounters();

        if (MyStatus.Alive)
        {
            //MyAnim.SetBool("Idle", true);
            if (atkDly <= 0)
            {
                AttackHandler();
            }
            if (DoAtk1)
            {
                BigAtk1();
            }
        }
    }


    void BigAtk1()
    {
        if (Atk1ShotDelay > 0)
        {
            Atk1ShotDelay -= 60 * Time.deltaTime;
        }
        if (Atk1ShotDelay <= 0 && Atk1Stage == 1)
        {
            MyAnim.SetTrigger("DoTP");
            Atk1Stage = 2;
            transform.position = TPPos + Vector2.up * 2;
            atk1StageDelay = 60;
        }
        if (Atk1Stage == 2)
        {
            if (atk1StageDelay <= 0)
            {
                Atk1Stage = 3;
            }
        }
        if (Atk1Stage == 3)
        {
            foreach (SpriteRenderer spr in Atk1RedZone)
            {
                spr.color = RedZoneWarn;
            }
            Atk1Stage = 4;
        }
        if (Atk1Stage == 4)
        {
            int done = 0;
            int i = 0;
            foreach (SpriteRenderer spr in Atk1RedZone)
            {
                if (Vector2.Distance(Atk1RedZoneFullScale[i], spr.transform.localScale) > .02f)
                {
                    spr.transform.localScale = Vector2.Lerp(spr.transform.localScale, Atk1RedZoneFullScale[i], 5 * Time.deltaTime * GameInfo.GM.GameSpeed);
                }
                else
                {
                    done++;
                    spr.transform.localScale = Atk1RedZoneFullScale[i];
                }
                i++;
            }
            if (done >= Atk1RedZone.Count - 1)
            {
                Atk1Stage = 5;
                atk1StageDelay = 60;
                
            }
        }
        if (Atk1Stage == 5)
        {
            foreach (SpriteRenderer spr in Atk1RedZone)
            {
                spr.color = Color.Lerp(spr.color, RedZoneHurt, .5f * Time.deltaTime * GameInfo.GM.GameSpeed);
            }
        }
        if (Atk1Stage == 6) //damage stage
        {

            if (Atk1ShotDelay <= 0)
            {
                ProjectileWithAngle(0 - 90, Atk1RedZone[0].transform.position, 60, false);
                ProjectileWithAngle(60 - 90, Atk1RedZone[1].transform.position, 60, false);
                ProjectileWithAngle(-60 - 90, Atk1RedZone[2].transform.position, 60, false);
                Atk1ShotDelay = 60;
                Scythe.SetTrigger("DoScythe");
                Atk1Stage = 7;
            }
        }
        if(Atk1Stage==7)
        {
            if (Atk1ShotDelay <= 0)
            {
                MyAnim.SetTrigger("DoScythe");
                Atk1Stage = 8;
                Atk1ShotDelay = 60;
            }
        }
        if (Atk1Stage == 8)
        {
            if (Atk1ShotDelay <= 0)
            {
                GameObject dmgAr = Instantiate(ScytheDamageArea);
                dmgAr.transform.position = Atk1RedZone[5].transform.position;
                Atk1SkullAnim = false;
                DoAtk1 = false;
                Atk1Stage = 0;
                Atk1Shots = 0;
                atkDly = 120;
                BigAtkDelay = 500;
                MyNav.StopToAttack = false;
            }
        }

    }

    public void StartTeleport()
    {
        transform.position = new Vector2(-999, -999);
    }

    void AttackHandler()
    {
        if (BigAtkDelay <= 0 && !DoAtk1)
        {
            Atk1Stage = 1;
            MyAnim.SetTrigger("StartTP");
            //Disable Collision?
            TPPos = GameInfo.PlayerPos;
            Instantiate(RingOfFire, GameInfo.PlayerPos, transform.rotation);
            Atk1ShotDelay = 90;
            MyNav.StopToAttack = true;
            DoAtk1 = true;
        }
        else if (!DoAtk1)
        {
            if (BigAtkDelay > 120)
            {

                ProjectileWithAngle(-90, Atk1RedZone[2].transform.position, 9999, true);
            }
            atkDly = 120;
        }
    }


    public void ProjectileWithAngle(float _angle, Vector2 _startPos, float _life, bool _homing)
    {
        LaunchPoint = transform;
        GameInfo.PlayAudio(AtkSfxIndex);
        GameObject proj = Instantiate(MyProjectile, _startPos, transform.localRotation);
        BasicProjectile BP = proj.GetComponent<BasicProjectile>();
        BP.life = _life;
        BP.TargetEnemy = false;
        BP.TargetPlayer = true;
        BP.dmg = MyDmg;
        proj.transform.Rotate(0, 0, _angle);
        proj.GetComponent<Rigidbody2D>().velocity = proj.transform.right * 17f;
        proj.transform.Rotate(0, 0, -_angle);
        if (_homing)
        {
            BP.Homing = true;
        }
    }

    void DoCounters()
    {
        if (atkDly > 0)
        {
            atkDly -= 60 * Time.deltaTime * GameInfo.GM.GameSpeed;
        }
        if (BigAtkDelay > 0)
        {
            BigAtkDelay -= 60 * Time.deltaTime * GameInfo.GM.GameSpeed;
            if (BigAtkDelay <= 60)
                MyNav.StopToAttack = true;
        }
        if (atk1StageDelay > 0)
        {
            atk1StageDelay -= 60 * Time.deltaTime * GameInfo.GM.GameSpeed;

            if(atk1StageDelay<=20)
            {
                if (!Atk1SkullAnim && Atk1Stage == 5)
                {
                    Atk1SkullAnim = true;
                    MyAnim.SetTrigger("DownSkull");
                }
            }

            if (atk1StageDelay <= 0)
            {
                Atk1Stage++;
                if (Atk1Stage == 6)
                {
                    Atk1Stage6Update();
                }
            }
        }
        if (Atk1ShotDelay > 0)
        {
            Atk1ShotDelay -= 60 * Time.deltaTime * GameInfo.GM.GameSpeed;
        }
    }
    void Atk1Stage6Update()
    {
        Instantiate(Bombs, Atk1RedZone[3].transform.position, transform.rotation);
        Instantiate(Bombs, Atk1RedZone[4].transform.position, transform.rotation);

        foreach (SpriteRenderer spr in Atk1RedZone)
        {
            //Atk1RedZoneFullScale.Add(spr.transform.localScale);
            spr.color = RedZoneWarn;
            spr.transform.localScale = Vector2.zero;
        }
    }
}
