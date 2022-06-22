using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBomb : MonoBehaviour
{
    public GameObject trail;
    public Transform Target;
    public int Dmg;
    public bool HitPlayer;
    public bool HitEnemies;
    public GameObject MyExplosion;
    public float TrailRate = 5;
    float trailCount = 5;
    public float FallSpeed = 5;

    // Update is called once per frame
    void Update()
    {
        if(trailCount>0)
        {
            trailCount -= 60 * Time.deltaTime;
            if(trailCount<=0)
            {
                Instantiate(trail, transform.position, transform.rotation);
                trailCount = TrailRate;
            }
        }

        if(Vector2.Distance(transform.position,Target.position) <= .25f)
        {
            AreaDamage exp = Instantiate(MyExplosion, transform.position, transform.rotation).GetComponent<AreaDamage>();
            exp.HitPlayer = HitPlayer;
            exp.HitNPC = HitEnemies;
            exp.Dmg = Dmg;
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            transform.Translate(-Vector3.up * 13 * Time.deltaTime);

        }
    }
}
