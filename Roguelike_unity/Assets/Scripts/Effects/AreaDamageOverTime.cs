using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamageOverTime : MonoBehaviour
{
    public float Dur = 90;
    public int Dmg = 1;
    public List<NPCStatus> AffectNPCs = new List<NPCStatus>();
    public PlayerStatus AffectPlayer = null;
    public bool HitPlayer = false;
    public bool HitNPC = false;
    public Animator MyAnim;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Dur >= 0)
        {
            Dur -= 60 * Time.deltaTime * GameInfo.GM.GameSpeed; ;
            if (Dur < 0)
            {
                DestroyOnComplete();
            }

            if (MyAnim != null)
                MyAnim.speed = GameInfo.GM.GameSpeed;
            else
                MyAnim = GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (HitPlayer && collision.transform.tag == "Player")
            collision.GetComponent<PlayerStatus>().DamagePlayer(Dmg);
        if (HitNPC && collision.transform.tag == "Enemy")
        {
            collision.GetComponent<NPCStatus>().TakeDmg(Dmg, 0);
        }
    }


    void GiveDamage()
    {

    }

    public void DestroyOnComplete()
    {
        GameObject.Destroy(this.gameObject);
    }
}
