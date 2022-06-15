using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Wave", order = 1)]
public class WaveData : ScriptableObject
{
    public List<GameObject> AvailableMonsters;
    public int SpawnHowMany;
    public float DelayBetweenSpawns;

    public List<GameObject> RelicEnemiesToSpawn;

    public int MaxAliveAtOnce = 125;
    public int ChanceOfRareChest;
    public int ChanceOfUniqueChest;
    public int ChanceOfLegendaryChest;

}
