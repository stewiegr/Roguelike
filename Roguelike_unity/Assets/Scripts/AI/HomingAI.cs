using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingAI : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer MySpr;
    NPCStatus MyStatus;
    Vector2 movement;
    Rigidbody2D MyRB;
    public float SpeedMod = .35f;
    public float SpeedModRandomize = .15f;
    float locateDel = 60;
    Vector2 desiredPos;
    Vector2 LastPos;
    Vector2 forcedVector;
    float KnockbackDur = 0;
    float rotTimer = 120;

    public bool InAtkRange = false;
    public bool FreeMove = true;

    bool launch = false;
    Vector2 launchTraj;
    Vector2 launchEnd;
    float UpTraj = 0;
    float rotAmt;

    bool xOb = false;
    bool yOb = false;

    void Start()
    {
        MyStatus = GetComponent<NPCStatus>();
        MyRB = GetComponent<Rigidbody2D>();
        MySpr = GetComponent<SpriteRenderer>();
        SpeedMod += Random.Range(-SpeedModRandomize, SpeedModRandomize);

        desiredPos.x = Random.Range(-.1f, .1f);
        desiredPos.y = Random.Range(-.1f, .1f);
        do
            rotAmt = Random.Range(-11, 11);
        while (Mathf.Abs(rotAmt) < Mathf.Abs(1));
    }

    // Update is called once per frame
    void Update()
    {
        if (MyStatus.Alive)
        {
            if (locateDel > 0)
                locateDel -= 60 * Time.deltaTime;
            else
                LocatePlayer();

            DoKnockback();
            DoSpriteFlip();
        }
        else
        {
            if (!launch)
            {
                StopMovement();
                if (rotTimer > 0)
                {
                    rotTimer -= 60 * Time.deltaTime;
                    if (rotTimer <= 0)
                    {
                        gameObject.SetActive(false);
                    }
                }
            }
            if(launch)
            {
                LaunchMath();
            }

        }

    }

    void DoMovement()
    {
        MyRB.velocity = ((Vector3)Vector2.ClampMagnitude(movement + forcedVector, MyStatus.RunSpeed));// * Time.deltaTime;     
    }

    void LaunchMath()
    {
        if (UpTraj > -3)
        {
            transform.position += Vector3.ClampMagnitude((Vector3)launchTraj + new Vector3(0, UpTraj, 0), 2) * Time.deltaTime;
            UpTraj -= .1f * 60 * Time.deltaTime;
            transform.eulerAngles += new Vector3(0, 0, rotAmt * 60 * Time.deltaTime);
        }
        else
        {
            MyStatus.MyAnim.speed = 1;
            MyStatus.MyAnim.SetTrigger("Die");
            MySpr.sortingLayerName = "Default";
            launch = false;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public void Knockback(float _duration)
    {
        KnockbackDur = _duration;
    }

    public void LaunchMe(Vector2 _dist)
    {
        launch = true;
        launchEnd = _dist;
        UpTraj = Random.Range(1.5f, 2.5f);
        launchTraj = _dist;
        launchTraj.x *= Random.Range(1.5f, 2.75f);
    }

    void DoKnockback()

    {
        if (KnockbackDur > 0)
        {
            FreeMove = false;
            KnockbackDur -= 60 * Time.deltaTime;
            MyRB.velocity = Vector2.ClampMagnitude(((Vector2)transform.position - GameInfo.PlayerPos).normalized * 2, KnockbackDur);
        }
        else
            FreeMove = true;
    }

    void DoSpriteFlip()
    {
        if (movement.x < 0)
            MySpr.flipX = false;
        if (movement.x > 0)
            MySpr.flipX = true;
        if (movement.x == 0)
        {
            if (GameInfo.PlayerPos.x > transform.position.x)
            {
                MySpr.flipX = true;
            }
            else
            {
                MySpr.flipX = false;
            }
        }
    }

    void LocatePlayer()
    {

        locateDel = Random.Range(10, 30);
        locateDel *= Mathf.Abs(Vector2.Distance(transform.position, GameInfo.PlayerPos));

        if (Mathf.Abs(Vector2.Distance(transform.position, GameInfo.PlayerPos)) < MyStatus.AtkRange)
            InAtkRange = true;
        else
            InAtkRange = false;


        if (!xOb && !yOb)
        {
            if (GameInfo.Player.position.x + desiredPos.x - transform.position.x < -.15f)
                movement.x = -MyStatus.RunSpeed;
            else if (GameInfo.Player.position.x + desiredPos.x - transform.position.x > .15f)
                movement.x = MyStatus.RunSpeed;
            else
                movement.x = 0;

            if (GameInfo.Player.position.y + desiredPos.y - transform.position.y < -.15f)
                movement.y = -MyStatus.RunSpeed;
            else if (GameInfo.Player.position.y + desiredPos.y - transform.position.y > .15f)
                movement.y = MyStatus.RunSpeed;
            else
                movement.y = 0;
        }

        movement.x *= SpeedMod;
        movement.y *= SpeedMod;


        LastPos = transform.position;

        if (movement != Vector2.zero)
            DoMovement();
        else
            MyRB.velocity = Vector2.zero;


    }

    public Vector2 GiveMovementVector()
    {
        return movement;
    }
    public void ForceVector(float x, float y)
    {
        locateDel = 2;
        forcedVector.x = x;
        forcedVector.y = y;
    }

    public void StopMovement()
    {
        movement = Vector2.zero;
        MyRB.velocity = Vector2.zero;
    }

}
