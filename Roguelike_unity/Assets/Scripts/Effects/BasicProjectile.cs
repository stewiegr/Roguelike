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

    public float life = 25;
    private void Update()
    {
        if (life > 0)
            life -= 60 * Time.deltaTime;
        if (life <= 0)
        {
            CreateExplosion(TargetPlayer, TargetEnemy);
            GameObject.Destroy(this.gameObject);
        }
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
