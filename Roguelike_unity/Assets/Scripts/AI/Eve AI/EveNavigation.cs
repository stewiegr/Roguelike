using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EveNavigation : MonoBehaviour
{
    public NPCStatus MyStatus;
    public Rigidbody2D MyRB;
    public Animator MyAnim;
    Vector2 PlayerPos;
    public int PreferredDistanceAway;
    public int PreferredDistanceClose;
    public int StrayMaxFromCenter = 22;
    public float MaxDashStrength;
    public float MaxTimeBetweenDash;
    public float MinTimeBetweenDash;
    float DashStrength;
    float MovementCD = 0;
    bool dashing = false;
    public bool StopToAttack;

    enum DirectionToDash
    {
        Toward,
        Away,
        Center
    }


    // Update is called once per frame
    void Update()
    {
        if (MyStatus.Alive)
        {
            if (!StopToAttack)
            {
                if (MovementCD <= 0)
                {
                    dashing = false;
                    MaintainDistance();

                }
                else
                {
                    MovementCD -= 60 * Time.deltaTime;
                }
            }
            else
            {
                MyRB.velocity = Vector2.zero;
            }

            }
    }

    private void FixedUpdate()
    {
        PlayerPos = GameInfo.PlayerPos;
        FacePlayer();
        if(dashing)
        DashAtrophy();
    }


    void MaintainDistance()
    {
        if (Random.Range(0, 10) > 6)
        {
            if (Vector2.Distance(transform.position, Vector2.zero) < StrayMaxFromCenter)
            {
                if (Vector2.Distance(transform.position, PlayerPos) > PreferredDistanceAway)
                {
                    Dash(DirectionToDash.Away);
                }
                if (Vector2.Distance(transform.position, PlayerPos) < PreferredDistanceClose)
                {
                    Dash(DirectionToDash.Toward);
                }
            }
            else
                Dash(DirectionToDash.Center);
        }
        else
        {
            if (Vector2.Distance(transform.position, Vector2.zero) > StrayMaxFromCenter)
            {
                Move(DirectionToDash.Center);
            }
            else
            {
                if (Vector2.Distance(transform.position, PlayerPos) > PreferredDistanceAway)
                {
                    Move(DirectionToDash.Away);
                }
                if (Vector2.Distance(transform.position, PlayerPos) < PreferredDistanceClose)
                {
                    Move(DirectionToDash.Toward);
                }
            }
        }
    }

    void Dash(DirectionToDash _dashWhere)
    {
        dashing = true;
        DashStrength = Random.Range(MaxDashStrength / 1.5f, MaxDashStrength);
        switch(_dashWhere)
        {
            case DirectionToDash.Away:
                MyRB.velocity = (GameInfo.PlayerPos - (Vector2)transform.position).normalized * DashStrength;
                break;
            case DirectionToDash.Toward:
                MyRB.velocity = ((Vector2)transform.position - GameInfo.PlayerPos).normalized * DashStrength;
                break;
            case DirectionToDash.Center:
                MyRB.velocity = (Vector2.zero - (Vector2)transform.position).normalized * DashStrength;
                break;
        }
        MovementCD = Random.Range(MinTimeBetweenDash, MaxTimeBetweenDash);
    }
    void Move(DirectionToDash _moveWhere)
    {
        dashing = false;
        switch (_moveWhere)
        {
            case DirectionToDash.Away:
                MyRB.velocity = (GameInfo.PlayerPos + new Vector2(Random.Range(-6,6), Random.Range(-6,6)) - (Vector2)transform.position).normalized * Random.Range(3,7);
                break;
            case DirectionToDash.Toward:
                MyRB.velocity = ((Vector2)transform.position + new Vector2(Random.Range(-6, 6), Random.Range(-6, 6)) - GameInfo.PlayerPos).normalized * Random.Range(3, 7); 
                break;
            case DirectionToDash.Center:
                MyRB.velocity = (Vector2.zero - (Vector2)transform.position).normalized * Random.Range(3, 7); 
                break;
        }
        MovementCD = Random.Range(MinTimeBetweenDash, MaxTimeBetweenDash);
    }

    void DashAtrophy()
    {
            MyRB.velocity *= Vector2.one * .98f;     
    }

    void FacePlayer()
    {

        if (PlayerPos.y < transform.position.y)
        {
            MyAnim.SetBool("Down", true);
            MyAnim.SetBool("Up", false);

            if (PlayerPos.y < transform.position.y - 6)
            {
                MyAnim.SetBool("Left", false);
                MyAnim.SetBool("Right", false);
            }
            else
            {
                if (PlayerPos.x < transform.position.x - 4)
                {
                    MyAnim.SetBool("Left", true);
                    MyAnim.SetBool("Right", false);
                    MyAnim.SetBool("Down", false);
                }
                else if (PlayerPos.x > transform.position.x + 4)
                {
                    MyAnim.SetBool("Left", false);
                    MyAnim.SetBool("Right", true);
                    MyAnim.SetBool("Down", false);
                }
                else
                {
                    MyAnim.SetBool("Left", false);
                    MyAnim.SetBool("Right", false);
                    MyAnim.SetBool("Down", true);
                }

            }

        }
        else
        {
            MyAnim.SetBool("Up", true);
            MyAnim.SetBool("Down", false);

            if ((Mathf.Abs(PlayerPos.x - transform.position.x)) > 7)
            {
                if (PlayerPos.x < transform.position.x)
                {
                    MyAnim.SetBool("Left", true);
                    MyAnim.SetBool("Right", false);
                    MyAnim.SetBool("Up", false);
                }
                if (PlayerPos.x > transform.position.x)
                {
                    MyAnim.SetBool("Left", false);
                    MyAnim.SetBool("Right", true);
                    MyAnim.SetBool("Up", false);
                }
            }
            else if (GameInfo.PlayerPos.y > transform.position.y + 2)
            {
                MyAnim.SetBool("Left", false);
                MyAnim.SetBool("Right", false);
            }
            else
            {
                if (PlayerPos.x < transform.position.x)
                {
                    MyAnim.SetBool("Left", true);
                    MyAnim.SetBool("Right", false);
                    MyAnim.SetBool("Up", false);
                }
                if (PlayerPos.x > transform.position.x)
                {
                    MyAnim.SetBool("Left", false);
                    MyAnim.SetBool("Right", true);
                    MyAnim.SetBool("Up", false);
                }
            }
        }
    }
}
