using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWorldObject : MonoBehaviour
{

    public GameObject DestroyEffect;
    public GameObject leaveBehind;
    
    public void DestroyMe(Vector2 incomingVecor)
    {
        AreaDamage exp = Instantiate(DestroyEffect, transform.position - new Vector3(0,0,3), transform.rotation).GetComponent<AreaDamage>();
        exp.Dmg = 0;
        exp.DoSpawnSeeds(incomingVecor);
        Instantiate(leaveBehind, transform.position, transform.rotation);
        GameObject.Destroy(this.gameObject);
    }

}
