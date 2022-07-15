using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{

    public void BombSpell(int _dmg, Vector3 _targetLoc)
    {
        BombObject bomb = Instantiate(GameInfo.EffectsDB.Bomb, _targetLoc + Vector3.up * 6, transform.rotation).GetComponent<BombObject>();
        bomb.InitBomb(_targetLoc.y, _dmg);
    }

}
