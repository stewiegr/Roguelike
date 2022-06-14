using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    public bool TargetPlayer;
    public bool TargetEnemy;
    public int dmg = 1;
    public GameObject myExplosion;
    public int Penetrations = 0;
    public int PenetrationMultiplier = 1;
    public float DefaultVel;
    public float Vel;

    public float life = 25;
    public bool Homing = false;
    Vector2 holdSpd;
    Transform homingTarg = null;
    Rigidbody2D myRB;

    private void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (life > 0)
            life -= 60 * Time.deltaTime * GameInfo.GM.GameSpeed;
        if (life <= 0)
        {
            CreateExplosion(TargetPlayer, TargetEnemy);
            GameObject.Destroy(this.gameObject);
        }

        if(Homing && GameInfo.GM.GameSpeed==1)
        {
            if(homingTarg!=null)
            {
                float angle = Mathf.Atan2(homingTarg.position.y - transform.position.y, homingTarg.position.x - transform.position.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, DefaultVel * 2 * Time.deltaTime);
                myRB.velocity = DefaultVel * transform.right;
            }
            else
            {
                AcquireTarget();
            }
        }

        if(GameInfo.GM.GameSpeed==0)
        {
            if (myRB.velocity != Vector2.zero)
            {
                holdSpd = myRB.velocity;
                myRB.velocity = Vector2.zero;
            }
        }
        else
        {
            if (myRB.velocity == Vector2.zero)
                myRB.velocity = holdSpd;
        }

    }

    public void AcquireTarget()
    {
        
        Transform closest= null;
        float dist=999;
        float newDist = 999;
        if (GameInfo.GM.LivingEnemies > 0)
        {
            foreach (GameObject monster in GameInfo.GM.CurrentLevel.GetActiveEnemies())
            {
                if (monster != null)
                {
                    newDist = Vector2.Distance(monster.transform.position, transform.position);
                    if (newDist < dist)
                    {
                        closest = monster.transform;
                        dist = newDist;
                    }
                }
            }
        }
        if (closest != null)
        {
            homingTarg = closest;
            Homing = true;
        }
        else
            homingTarg = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool breakout = false;
        {
            if ((TargetEnemy && collision.transform.tag == "Enemy" || TargetPlayer &&  collision.transform.tag == "Player"))
            {
                if(TargetEnemy)
                {
                    if(collision.GetComponent<NPCStatus>().Shielded && !breakout)
                    {
                        myRB.velocity = new Vector2(myRB.velocity.x * -.7f, myRB.velocity.y * -.7f);
                        life = 90;
                        Homing = false;
                        TargetPlayer = true;
                        TargetEnemy = false;
                        GetComponent<SpriteRenderer>().color = Color.red;
                        breakout = true;
                    }
                }
                if (TargetPlayer && !breakout)
                {
                    if(collision.GetComponent<PlayerStatus>().Shielded)
                    {
                        myRB.velocity = new Vector2(myRB.velocity.x * -1, myRB.velocity.y * -1);
                        TargetPlayer = false;
                        life = 90;
                        Homing = false;
                        TargetEnemy = true;
                        GetComponent<SpriteRenderer>().color = Color.green;
                        breakout = true;
                    }
                }
                if (!breakout)
                {
                    CreateExplosion(TargetPlayer, TargetEnemy);
                    if (Penetrations <= 0)
                    {
                        GameObject.Destroy(this.gameObject);
                    }
                    else
                    {
                        Penetrations--;
                    }
                }
            }
            if(collision.transform.tag=="WorldObject")
            {
                CreateExplosion(TargetPlayer, TargetEnemy);
                GameObject.Destroy(this.gameObject);
            }
        }
    }


    void CreateExplosion(bool _player, bool _enemy)
    {
        AreaDamage AE = Instantiate(myExplosion, transform.position, transform.rotation).GetComponent<AreaDamage>();
        AE.Dmg = dmg;
        AE.HitPlayer = _player;
        AE.HitNPC = _enemy;

    }


}
