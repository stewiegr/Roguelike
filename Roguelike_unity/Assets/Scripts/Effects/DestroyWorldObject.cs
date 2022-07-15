using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWorldObject : MonoBehaviour
{

    public GameObject DestroyEffect;
    public GameObject leaveBehind;
    public Vector3 offsetSpawn;
    public int modDmg = 0;
    
    public void DestroyMe(Vector2 incomingVecor, int _expDmg)
    {
        AreaDamage exp = Instantiate(DestroyEffect, transform.position - new Vector3(0,0,3) + offsetSpawn, transform.rotation).GetComponent<AreaDamage>();
        if (modDmg == 0)
            exp.Dmg = _expDmg;
        else
            exp.Dmg = modDmg;
        exp.HitNPC = true;
        exp.DoSpawnSeeds(incomingVecor);
        Instantiate(leaveBehind, transform.position, transform.rotation);
        GameObject.Destroy(this.gameObject);
    }

}
