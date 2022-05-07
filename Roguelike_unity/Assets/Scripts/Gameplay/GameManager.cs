using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentKillsThisWave = 0;
    public int LivingEnemies = 0;

    private void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        GameInfo.GM = this;
    }
}
