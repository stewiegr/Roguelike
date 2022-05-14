using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    GameManager GM;

    [Tooltip("Monsters that can spawn")]
    public List<GameObject> Monsters;
    [Tooltip("Delay in frames between each monster spawn")]
    public float SpawnDelay=10;
    [Tooltip("How many waves in this level")]
    public int Waves=3;
    [Tooltip("Maximum number of monsters that could be in a wave")]
    public int MaxPerWave=200;
    [Tooltip("Minimum number of monsters that could be in a wave")]
    public int MinPerWave=120;
    [Tooltip("Multiplier - can more monsters spawn in each wave?")]
    public float WaveModifier=1.25f;

    [Tooltip("Upper Left Limit of Spawn Area")]
    public Transform UpperLeftBounds;
    [Tooltip("Lower Right Limit of Spawn Area")]
    public Transform LowerRightBounds;
    [Tooltip("PLACEHOLDER - spawn a chest when done")]
    public GameObject RewardChest;

    int spawnedSoFar = 0;
    int setSpawnNumber;
    bool waveStarted = false;
    int enemiesKilled = 0;
    public int currentWave = 0;
    float delay;
    bool treasureWave = false;
    bool chestUp = false;
    [Tooltip("1.5-5 is a good range")]
    public float ForcedDistanceFromPlayer = 1.5f;

    private void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        delay = SpawnDelay;
    }
    // Update is called once per frame
    void Update()
    {
        if(!waveStarted && currentWave<Waves)
        {
            InitWave();
        }
        else if(waveStarted && GM.currentKillsThisWave<setSpawnNumber-4)
        {
            if (spawnedSoFar <= setSpawnNumber)
            {
                if (delay > 0)
                {
                    delay -= 60 * Time.deltaTime;
                    if (delay <= 0)
                    {
                        SpawnMonster();
                    }
                }
            }
        }
        if(GM.currentKillsThisWave>=setSpawnNumber)
        {
            waveStarted = false;
        }
        if(currentWave>=Waves && GM.LivingEnemies<=0 && !chestUp)
        {
            chestUp = true;
            RewardChest.transform.position = GameInfo.PlayerPos + new Vector2(Random.Range(1f, 2f), Random.Range(1f, 2f));
            RewardChest.gameObject.SetActive(true);
            //CamID.Cam.ShakeScreen(2, 5);
        }
    }

    void SpawnMonster()
    {
        Vector2 pos;
        do
        {
            pos = new Vector2(Random.Range(UpperLeftBounds.position.x, LowerRightBounds.position.x), Random.Range(UpperLeftBounds.position.y, LowerRightBounds.position.y));
        }
        while (Mathf.Abs(Vector2.Distance(GameInfo.PlayerPos, pos)) < ForcedDistanceFromPlayer || Physics2D.OverlapCircleAll(pos, 2).Length != 0);

        GameObject NPC = Instantiate(Monsters[Random.Range(0, Monsters.Count)], pos, transform.rotation);
        NPC.GetComponent<NPCStatus>().GM = GM;
        delay = SpawnDelay;
        spawnedSoFar++;
        GM.LivingEnemies++;
    }

    void InitWave()
    {
        currentWave++;
        GM.currentKillsThisWave = 0;
        setSpawnNumber = Random.Range(MinPerWave, MaxPerWave) * (int)(currentWave * WaveModifier);
        spawnedSoFar = 0;
        waveStarted = true;
    }
}
