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
    Transform homingTarg = null;
    Rigidbody2D myRB;

    private void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (life > 0)
            life -= 60 * Time.deltaTime;
        if (life <= 0)
        {
            CreateExplosion(TargetPlayer, TargetEnemy);
            GameObject.Destroy(this.gameObject);
        }

        if(Homing)
        {
            if(homingTarg!=null)
            {
                float angle = Mathf.Atan2(homingTarg.position.y - transform.position.y, homingTarg.position.x - transform.position.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 720 * Time.deltaTime);
                myRB.velocity = DefaultVel * transform.right;
            }
            else
            {
                AcquireTarget();
            }
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
        {
            if ((TargetEnemy && collision.transform.tag == "Enemy" || TargetPlayer &&  collision.transform.tag == "Player"))
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
