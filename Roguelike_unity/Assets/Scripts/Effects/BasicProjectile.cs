using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    public bool TargetPlayer;
    public bool TargetEnemy;
    public int dmg = 1;
    public GameObject myExplosion;
    public GameObject SecondaryExplosion;
    public int Penetrations = 0;
    public int PenetrationMultiplier = 1;
    public float DefaultVel;
    public float Vel;
    public bool CanBeDestroyed;

    public float life = 25;
    public bool Homing = false;
    Vector2 holdSpd;
    Transform homingTarg = null;
    Rigidbody2D myRB;
    Vector3 Direction;

    public List<Sprite> EightDirections = new List<Sprite>();
    public SpriteRenderer MySpr;

    public GameObject Trail;
    Vector2 LastTrailDrop;
    bool angled = false;

    public Vector2 Velocity;
    public float accelMod = 0;
    Vector2 setVel = Vector2.zero;
    public float HomingRate = 1;

    public float ScreenShakeAmt = 0;
    public float ScreenShakeDur = 0;

    private void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        MySpr = GetComponent<SpriteRenderer>();
        LastTrailDrop = transform.position;
        //life = life * accelMod;
    }


    private void Update()
    {
        if (accelMod != 0 && !Homing)
        {
            if (setVel == Vector2.zero)
            {
                setVel = myRB.velocity;
                myRB.velocity *= Vector2.one * .05f;
            }
            if(!Homing)
            DoAccelEffect();
        }
            Velocity = myRB.velocity;
        if (life > 0)
        {
            life -= 60 * Time.deltaTime * GameInfo.GM.GameSpeed;
        }
        if (Trail != null)
        {
            if (Vector2.Distance(transform.position, LastTrailDrop) >= 1)
            {
                LastTrailDrop = transform.position;
                Instantiate(Trail, transform.position, transform.rotation);
            }
        }

        if (life <= 0)
        {
            CreateExplosion(TargetPlayer, TargetEnemy);
            GameObject.Destroy(this.gameObject);
        }

        if (Homing && GameInfo.GM.GameSpeed == 1)
        {
            if (homingTarg != null)
            {

                Direction = Vector3.Normalize(homingTarg.position - transform.position);
                if (accelMod == 0)
                {
                    myRB.velocity = Vector3.Lerp(myRB.velocity, DefaultVel * Direction, 3 * HomingRate * Time.deltaTime);
                }
                else
                {
                    myRB.velocity = Vector3.Lerp(myRB.velocity, DefaultVel * Direction, 3 * HomingRate * Time.deltaTime);
                }
            }
            else
            {
                AcquireTarget();
            }
        }

        if (GameInfo.GM.GameSpeed == 0)
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

        if (EightDirections.Count != 0)
        {
            DoDirectionGraphics();
        }
        else
        {
            if (Homing || !angled)
            {
                transform.right = myRB.velocity;
                angled = true;
            }
        }

    }

    void DoAccelEffect()
    {
        myRB.velocity = Vector2.Lerp(myRB.velocity, setVel, Time.deltaTime * accelMod);
    }

    public void AcquireTarget()
    {
        if (TargetEnemy)
        {
            Transform closest = null;
            float dist = 999;
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
        else if (TargetPlayer)
            homingTarg = GameInfo.Player;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool breakout = false;

        if (CanBeDestroyed)
        {
            if (collision.transform.tag == "Projectile")
            {
                if (collision.GetComponent<BasicProjectile>() != null)
                {
                    BasicProjectile other = collision.GetComponent<BasicProjectile>();

                    if ((TargetPlayer && other.TargetEnemy) || (TargetEnemy && other.TargetPlayer))
                    {
                        Penetrations = 0;
                        CreateExplosion(TargetPlayer, TargetEnemy);
                        GameObject.Destroy(this.gameObject);
                    }
                }
            }
        }

        if ((TargetEnemy && collision.transform.tag == "Enemy" || TargetPlayer && collision.transform.tag == "Player"))
        {
            if (TargetEnemy)
            {
                if (collision.GetComponent<NPCStatus>().Shielded && !breakout)
                {
                    myRB.velocity = new Vector2(myRB.velocity.x * -.7f, myRB.velocity.y * -.7f);
                    setVel = myRB.velocity;
                    accelMod = 0;
                    life = 90;
                    dmg = 1;
                    Homing = false;
                    TargetPlayer = true;
                    TargetEnemy = false;
                    GetComponent<SpriteRenderer>().color = Color.red;
                    breakout = true;
                }
            }
            if (TargetPlayer && !breakout)
            {
                if (collision.GetComponent<PlayerStatus>().Shielded)
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
        if (collision.transform.tag == "WorldObject")
        {

            if(collision.transform.GetComponent<DestroyWorldObject>()!=null)
            {
                collision.transform.GetComponent<DestroyWorldObject>().DestroyMe(myRB.velocity);
            }
            else
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

        if (ScreenShakeAmt != 0)
            CamID.CMController.ShakeScreen(ScreenShakeAmt, ScreenShakeDur);

        if (SecondaryExplosion != null)
            Instantiate(SecondaryExplosion, transform.position, transform.rotation);


    }

    void DoDirectionGraphics()
    {
        //0 = down left
        //1 = down
        //2 = down right
        //3 = right
        //4 = right up
        //5 = up
        //6 = up left
        // 7 = left


        if (Mathf.Abs(myRB.velocity.x) < Mathf.Abs(myRB.velocity.y * .5f) && myRB.velocity.y > 0)
            MySpr.sprite = EightDirections[5];
        else if (Mathf.Abs(myRB.velocity.x) < Mathf.Abs(myRB.velocity.y * .5f) && myRB.velocity.y < 0)
            MySpr.sprite = EightDirections[1];
        else if (Mathf.Abs(myRB.velocity.y) < Mathf.Abs(myRB.velocity.x * .5f) && myRB.velocity.x < 0)
            MySpr.sprite = EightDirections[7];
        else if (Mathf.Abs(myRB.velocity.y) < Mathf.Abs(myRB.velocity.x * .5f) && myRB.velocity.x > 0)
            MySpr.sprite = EightDirections[3];
        else if (myRB.velocity.y > 0 && myRB.velocity.x > 0)
            MySpr.sprite = EightDirections[4];
        else if (myRB.velocity.y < 0 && myRB.velocity.x < 0)
            MySpr.sprite = EightDirections[0];
        else if (myRB.velocity.y > 0 && myRB.velocity.x < 0)
            MySpr.sprite = EightDirections[6];
        else if (myRB.velocity.y < 0 && myRB.velocity.x > 0)
            MySpr.sprite = EightDirections[2];


    }


}
