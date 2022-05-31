using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamage : MonoBehaviour
{
    public float Dur = 3;
    public int Dmg = 1;
    public float force=20;
    public List<NPCStatus> AffectNPCs = new List<NPCStatus>();
    public PlayerStatus AffectPlayer=null;
    public bool HitPlayer = false;
    public bool HitNPC = false;
    public Animator MyAnim;
    public bool DeactivateDontDestroy;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Dur>=0)
        {
            Dur -= 60 * Time.deltaTime * GameInfo.GM.GameSpeed;
            if(Dur<0)
            {
                GiveDamage();
            }
        }

        if (MyAnim != null)
            MyAnim.speed = GameInfo.GM.GameSpeed;
        else
            MyAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (HitPlayer && collision.transform.tag == "Player")
            AffectPlayer = collision.GetComponent<PlayerStatus>();
        if(HitNPC && collision.transform.tag=="Enemy")
        {
            NPCStatus hit = collision.GetComponent<NPCStatus>();
            if (!AffectNPCs.Contains(hit))
                AffectNPCs.Add(hit);
        }
    }

    void GiveDamage()
    {
        //CamID.Cam.ShakeScreen(1f, 2);
        if (AffectPlayer != null)
            AffectPlayer.DamagePlayer(Dmg);
        if(AffectNPCs.Count>0)
        {
            foreach(NPCStatus NPC in AffectNPCs)
            {
                NPC.TakeDmg(Dmg,force);
            }
        }
    }

    public void DestroyOnComplete()
    {
        if (!DeactivateDontDestroy)
            GameObject.Destroy(this.gameObject);
        else
        {
            Dur = 3;
            AffectNPCs.Clear();
            AffectPlayer = null;
            gameObject.SetActive(false);
            
        }
    }
}
