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

    bool DoAtk1;
    int Atk1Shots = 0;
    float Atk1Angle;
    float Atk1ShotDelay = 20;

    bool DoAtk2;
    int Atk2Shots = 0;
    float Atk2Angle;
    float Atk2ShotDelay = 20;


    private void Start()
    {
        //GetComponent<EveNavigation>();

    }
    void Update()
    {
        if (atkDly > 0)
        {
            atkDly -= 60 * Time.deltaTime * GameInfo.GM.GameSpeed;
        }
        if (BigAtkDelay > 0)
        {
            BigAtkDelay -= 60 * Time.deltaTime * GameInfo.GM.GameSpeed;
        }
        if (MyStatus.Alive)
        {
            MyAnim.SetBool("Idle", true);
            if (atkDly <= 0)
            {
                AttackHandler();
            }
            if (DoAtk1)
            {
                BigAtk1();
            }
            if (DoAtk2)
            {
                BigAtk2();
            }
        }
    }

    void BigAtk2()
    {
        atkDly = 180;
        if (Atk2Shots <= 12)
        {
            if (Atk2ShotDelay <= 0)
            {
                Atk2ShotDelay = 10;
                SpawnMultipleProjectileWithVariation(Random.Range(5,30));
                Atk2Angle += 10;
                Atk2Shots++;
            }
            else
            {
                Atk2ShotDelay -= 60 * Time.deltaTime;
            }
        }
        else
        {
            DoAtk2 = false;
            MyNav.StopToAttack = false;
            atkDly = 30;
            BigAtkDelay = Random.Range(300, 600);
            Atk2Shots = 0;
        }
    }

    void BigAtk1()
    {
        atkDly = 180;
        if (Atk1Shots <= 128)
        {
            if (Atk1ShotDelay <= 0)
            {
                Atk1ShotDelay = 1;
                ProjectileWithAngle(Atk1Angle);
                Atk1Angle += Random.Range(8,13);
                Atk1Shots++;
            }
            else
            {
                Atk1ShotDelay -= 60 * Time.deltaTime;
            }
        }
        else
        {
            DoAtk1 = false;
            MyNav.StopToAttack = false;
            atkDly = 30;
            BigAtkDelay = Random.Range(300, 600);
            Atk1Shots = 0;
        }
    }

    void AttackHandler()
    {
        if (BigAtkDelay > 0)
        {
            if (Random.Range(0, 10) > 5)
            {
                MyAnim.SetTrigger("Attack");
                if (Random.Range(0, 10) < 7)
                    atkDly = MyStatus.AtkDly;
                else
                    atkDly = 25;
                SpawnProjectile(LaunchPointPositionR);
            }
        }
        else
        {
            if (Random.Range(0, 10) > 5)
            {
                Atk1ShotDelay = 60;
                MyNav.StopToAttack = true;
                DoAtk1 = true;
                Atk1Angle = Random.Range(0, 360);

            }
            else
            {
                Atk2ShotDelay = 60;
                DoAtk2 = true;
            }
        }
    }

    public void SpawnProjectile(Transform LaunchPos)
    {
        if (LaunchPoint == LaunchPointPositionL)
            LaunchPoint = LaunchPointPositionR;
        else
            LaunchPoint = LaunchPointPositionL;
        GameInfo.PlayAudio(AtkSfxIndex);

        //transform.eulerAngles = new Vector3(0, 0, angle);
        GameObject proj = Instantiate(MyProjectile, LaunchPoint.position, transform.localRotation);
        Vector2 direction = GameInfo.Player.position - (proj.transform.position);
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        proj.transform.eulerAngles = new Vector3(0, 0, angle);
        BasicProjectile BP = proj.GetComponent<BasicProjectile>();
        BP.life = 400;
        BP.TargetEnemy = false;
        BP.TargetPlayer = true;
        BP.dmg = 1;
        proj.GetComponent<Rigidbody2D>().velocity = proj.transform.right * 17f;
    }

    public void SpawnMultipleProjectileWithVariation(float variation)
    {
        if (LaunchPoint == LaunchPointPositionL)
            LaunchPoint = LaunchPointPositionR;
        else
            LaunchPoint = LaunchPointPositionL;
        GameInfo.PlayAudio(AtkSfxIndex);

        //transform.eulerAngles = new Vector3(0, 0, angle);

        for (int i = 0; i <= 2; i++)
        {
            GameObject proj = Instantiate(MyProjectile, LaunchPoint.position, transform.localRotation);
            Vector2 direction = GameInfo.Player.position - (proj.transform.position);
            float angle = Vector2.SignedAngle(Vector2.right, direction);
            if (i == 0)
                proj.transform.eulerAngles = new Vector3(0, 0, angle);
            if (i == 1)
                proj.transform.eulerAngles = new Vector3(0, 0, angle+variation);
            if (i == 2)
                proj.transform.eulerAngles = new Vector3(0, 0, angle - variation);
            BasicProjectile BP = proj.GetComponent<BasicProjectile>();
            BP.life = 400;
            BP.TargetEnemy = false;
            BP.TargetPlayer = true;
            BP.dmg = 1;
            proj.GetComponent<Rigidbody2D>().velocity = proj.transform.right * 17f;
        }
    }

    public void ProjectileWithAngle(float _angle)
    {
        if (LaunchPoint == LaunchPointPositionL)
            LaunchPoint = LaunchPointPositionR;
        else
            LaunchPoint = LaunchPointPositionL;
        GameInfo.PlayAudio(AtkSfxIndex);
        //transform.eulerAngles = new Vector3(0, 0, angle);
        GameObject proj = Instantiate(MyProjectile, LaunchPoint.position, transform.localRotation);
        proj.transform.eulerAngles = new Vector3(0, 0, _angle);
        BasicProjectile BP = proj.GetComponent<BasicProjectile>();
        BP.life = 400;
        BP.TargetEnemy = false;
        BP.TargetPlayer = true;
        BP.dmg = 1;
        proj.GetComponent<Rigidbody2D>().velocity = proj.transform.right * 17f;
    }


}
