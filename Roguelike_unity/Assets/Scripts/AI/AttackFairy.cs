using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFairy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MyProjectile;
    float attackCD = 60;
    public Transform Player;
    Transform homingTarg;
    float navCD = 20;
    Vector2 offset;
    SpriteRenderer MySpr;
    Animator MyAnim;

    private void Start()
    {
        MySpr = GetComponent<SpriteRenderer>();
        MyAnim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (attackCD > 0)
        {
            attackCD -= 60 * Time.deltaTime * GameInfo.GM.GameSpeed;
            if (attackCD <= 0)
            {
                AcquireTarget();
                attackCD = 60;

                if (homingTarg != null)
                    DoAttack();
            }
        }

        if (GameInfo.GM.GameSpeed == 1)
        {
            Nav();
            MyAnim.speed = 1;
        }
        else
        {
            MyAnim.speed = 0;
        }
        if (navCD > 0)
        {
            navCD -= 60 * Time.deltaTime * GameInfo.GM.GameSpeed;
            if (navCD <= 0)
            {
                offset = new Vector2(Random.Range(-2, 2), Random.Range(-2, 4));
                navCD = Random.Range(30, 60);
                MySpr.flipX = !MySpr.flipX;
            }
        }
    }

    void DoAttack()
    {
        Vector2 direction = homingTarg.position - transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        //transform.eulerAngles = new Vector3(0, 0, angle);
        GameObject proj = Instantiate(MyProjectile, transform.position + Vector3.up * .35f, transform.localRotation);
        proj.transform.eulerAngles = new Vector3(0, 0, angle);
        BasicProjectile BP = proj.GetComponent<BasicProjectile>();
        BP.life = 100;
        BP.TargetEnemy = true;
        BP.dmg = 6;
        proj.GetComponent<Rigidbody2D>().velocity = proj.transform.right * 15.5f;
        // BP.Homing = true;
        // BP.AcquireTarget();
    }

    void Nav()
    {
        
        transform.position = (Vector2.Lerp((Vector2)transform.position, (Vector2)Player.transform.position + offset, .75f * GameInfo.PlayerStatus.RunSpeed * Time.deltaTime * GameInfo.GM.GameSpeed));
    }

    public void AcquireTarget()
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
                    if (newDist < dist && newDist < 10)
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
        }
        else
            homingTarg = null;

    }
}
