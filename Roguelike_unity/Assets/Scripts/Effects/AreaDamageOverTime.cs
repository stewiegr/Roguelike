using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamageOverTime : MonoBehaviour
{
    public float Dur = 90;
    public float Ticks = 30;
    public int Dmg = 1;
    public List<NPCStatus> AffectNPCs = new List<NPCStatus>();
    public PlayerStatus AffectPlayer = null;
    public bool HitPlayer = false;
    public bool HitNPC = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Dur >= 0)
        {
            Dur -= 60 * Time.deltaTime;
            if (Dur < 0)
            {
                DestroyOnComplete();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (HitPlayer && collision.transform.tag == "Player")
            AffectPlayer.DamagePlayer(Dmg);
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
